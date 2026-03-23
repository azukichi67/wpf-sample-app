using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleApp.UI.Views;

namespace SampleApp.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly CounterMvvmPage _counterMvvmPage;
        private readonly CounterBehidePage _counterBehindPage;
        private readonly ExcelToCsvPage _excelToCsvPage;

        [ObservableProperty]
        private Page? _currentPage;

        public MainViewModel()
        {
            _counterMvvmPage = new CounterMvvmPage();
            _counterBehindPage = new CounterBehidePage();
            _excelToCsvPage = new ExcelToCsvPage();
        }

        [RelayCommand]
        private void MoveCounterMvvm()
        {
            CurrentPage = _counterMvvmPage;
        }

        [RelayCommand]
        private void MoveCounterCodeBehind()
        {
            CurrentPage = _counterBehindPage;
        }

        [RelayCommand]
        private void MoveExcelToCsv()
        {
            CurrentPage = _excelToCsvPage;
        }
    }
}
