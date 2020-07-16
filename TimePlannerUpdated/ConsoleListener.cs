using System;
using System.Threading;

namespace TimePlannerUpdated.Terminal
{
    class ConsoleListener
    {
        public static event ConsoleMouseEvent MouseEvent;

        public static event ConsoleKeyEvent KeyEvent;

        public static event ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

        // Own created Events
        public static event ConsoleMouseEvent ClickEvent;
        public static event ConsoleMouseEvent ScrollUpEvent;
        public static event ConsoleMouseEvent ScrollDownEvent;


        private static bool Run = false;

        public static void Setup()
        {
            var handle = GetStdHandle(STD_INPUT_HANDLE);

            uint mode = 0;
            if (!(GetConsoleMode(handle, ref mode))) { throw new Exception("GetConsoleMode error"); }

            mode |= ENABLE_MOUSE_INPUT;
            mode &= ~ENABLE_QUICK_EDIT_MODE;
            mode |= ENABLE_EXTENDED_FLAGS;

            if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Exception("SetConsoleMode error"); }

            //IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
            //uint mode = 0;
            //GetConsoleMode(inHandle, ref mode);
            //mode &= ~ENABLE_QUICK_EDIT_MODE; //disable
            //mode |= ENABLE_WINDOW_INPUT; //enable (if you want)
            //mode |= ENABLE_MOUSE_INPUT; //enable
            //SetConsoleMode(inHandle, mode);
        }
        public static void Start()
        {
            if (!Run)
            {
                Run = true;
                IntPtr handleIn = GetStdHandle(STD_INPUT_HANDLE);
                new Thread(() =>
                {
                    while (true)
                    {
                        uint numRead = 0;
                        INPUT_RECORD[] record = new INPUT_RECORD[1];
                        record[0] = new INPUT_RECORD();
                        ReadConsoleInput(handleIn, record, 1, ref numRead);
                        if (Run)
                            switch (record[0].EventType)
                            {
                                case INPUT_RECORD.MOUSE_EVENT:
                                    MouseEvent?.Invoke(record[0].MouseEvent);
                                    switch (record[0].MouseEvent.dwButtonState)
                                    {
                                        case MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED:
                                            ClickEvent?.Invoke(record[0].MouseEvent);
                                            break;
                                        case MOUSE_EVENT_RECORD.SCROLLED_UP:
                                            ScrollUpEvent?.Invoke(record[0].MouseEvent);
                                            break;
                                        case MOUSE_EVENT_RECORD.SCROLLED_DOWN:
                                            ScrollDownEvent?.Invoke(record[0].MouseEvent);
                                            break;
                                    }
                                    break;
                                case INPUT_RECORD.KEY_EVENT:
                                    KeyEvent?.Invoke(record[0].KeyEvent);
                                    break;
                                case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                                    WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                                    break;
                            }
                        else
                        {
                            uint numWritten = 0;
                            WriteConsoleInput(handleIn, record, 1, ref numWritten);
                            return;
                        }
                    }
                }).Start();
            }
        }

        public static void Stop() => Run = false;


        public delegate void ConsoleMouseEvent(MOUSE_EVENT_RECORD r);

        public delegate void ConsoleKeyEvent(KEY_EVENT_RECORD r);

        public delegate void ConsoleWindowBufferSizeEvent(WINDOW_BUFFER_SIZE_RECORD r);
    }
}
