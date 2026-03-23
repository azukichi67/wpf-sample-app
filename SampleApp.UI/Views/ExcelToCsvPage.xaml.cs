using System.Windows.Controls;
using SampleApp.UI.ViewModels;

namespace SampleApp.UI.Views
{
    public partial class ExcelToCsvPage : Page
    {
        public ExcelToCsvPage()
        {
            InitializeComponent();
            DataContext = new ExcelToCsvViewModel();
        }
    }
}
