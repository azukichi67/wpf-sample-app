using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleApp.Core.Services.Excel;

namespace SampleApp.UI.ViewModels
{
    public partial class ExcelToCsvViewModel : ObservableObject
    {
        private readonly ExcelService _excelService;

        public ExcelToCsvViewModel()
        {
            _excelService = new ExcelService();
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertCommand))]
        private string _excelFilePath = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertCommand))]
        private string _csvFilePath = string.Empty;

        [ObservableProperty]
        private string _statusMessage = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertCommand))]
        private bool _isConverting;

        private bool CanConvert() =>
            !string.IsNullOrEmpty(ExcelFilePath)
            && !string.IsNullOrEmpty(CsvFilePath)
            && !IsConverting;

        [RelayCommand(CanExecute = nameof(CanConvert))]
        private async Task ConvertAsync()
        {
            IsConverting = true;
            StatusMessage = "変換中...";

            await Task.Run(() => _excelService.ConvertToCsv(new CovertToCsvParam
            {
                ExcelFilePath = ExcelFilePath,
                CsvFilePath = CsvFilePath,
            }));

            IsConverting = false;
            StatusMessage = "変換完了";
        }
    }
}
