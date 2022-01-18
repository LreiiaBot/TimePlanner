using System.Windows.Controls;
using TimePlannerUpdated.Default;

namespace TimePlannerUpdated.WPFGUI.Pages
{
    /// <summary>
    /// Interaktionslogik für AutoAddPage.xaml
    /// </summary>
    public partial class AutoAddPage : Page
    {
        public TimeControlledElement Selected { get; set; } = new TaskList("kek", "kokdesc");
        public AutoAddPage()
        {
            InitializeComponent();
        }

        public AutoAddPage(TimeControlledElement selected)
        {
            //Selected = selected;
        }

        public AutoAddPage(object viewModel)
        {
            //this.DataContext = viewModel;
        }
    }
}
