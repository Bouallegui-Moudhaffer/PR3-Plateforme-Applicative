using PA.Desktop.Services;
using PA.Desktop.ViewModel;
using System.Windows.Controls;

namespace PA.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel(SystemInfoService.Instance);
        }
    }
}
