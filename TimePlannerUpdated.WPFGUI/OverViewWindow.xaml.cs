using System.Windows;
using System.Windows.Controls;
using TimePlannerUpdated.Default;

namespace TimePlannerUpdated.WPFGUI
{
    /// <summary>
    /// Interaktionslogik für OverViewWindow.xaml
    /// </summary>
    public partial class OverViewWindow : Window
    {
        private MainViewModel mv = null;
        public OverViewWindow()
        {
            mv = (MainViewModel)FindResource("mvm");
            this.DataContext = mv;
            mv.DialogCreateRequest += OnDialogCreateRequest;
            mv.MessageBoxRequest += OnMessageBoxRequest;

            InitializeComponent();
            tbDescription.Focus();
        }

        private void OnDialogCreateRequest(object sender, MvvmDialogEventArgs e)
        {
            Window w = null;

            switch (e.type)
            {
                case DialogType.ViewLists:
                    w = new ListViewWindow(mv);
                    break;
                case DialogType.Detail:
                    // todo w = new DetailWindow();
                    break;
                case DialogType.Edit:
                    // todo w = new EditWindow();
                    break;
                default:
                    MessageBox.Show($"unknown dialogtype {e.type}");
                    break;
            }

            e.actionAfterCreate?.Invoke();

            if (w != null)
            {
                w.ShowDialog();
            }
        }

        private void OnMessageBoxRequest(object sender, MvvmMessageBoxEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(e.MessageBoxText, e.Caption, e.OnlyOk ? MessageBoxButton.OK : MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);

            System.Console.WriteLine(result);

            e.ResultAction?.Invoke(result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            cbLists.IsEnabled = ((CheckBox)sender).IsChecked == false;
        }
    }
}
