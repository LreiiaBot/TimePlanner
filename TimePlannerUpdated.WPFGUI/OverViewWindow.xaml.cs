using System.Windows;

namespace TimePlannerUpdated.WPFGUI
{
    /// <summary>
    /// Interaktionslogik für OverViewWindow.xaml
    /// </summary>
    public partial class OverViewWindow : Window
    {
        public OverViewWindow()
        {
            InitializeComponent();
            tbDescription.Focus();
        }
    }
}
