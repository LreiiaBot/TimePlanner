using System.Windows;
using TimePlannerUpdated.Default;

namespace TimePlannerUpdated.WPFGUI
{
    /// <summary>
    /// Interaktionslogik für ListViewWindow.xaml
    /// </summary>
    public partial class ListViewWindow : Window
    {
        private MainViewModel mv;
        public ListViewWindow(MainViewModel mv)
        {
            this.mv = mv;
            mv.EnableInputRequest += OnEnableInputRequest;
            InitializeComponent();
        }

        /// <summary>
        /// Sets the UIElements up for input or restricts it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">true if userinput should be possible</param>
        private void OnEnableInputRequest(object sender, bool e)
        {
            tbTitle.IsEnabled = e;
            tbDescription.IsEnabled = e;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mv.inOverViewWindow = true;
        }
    }
}
