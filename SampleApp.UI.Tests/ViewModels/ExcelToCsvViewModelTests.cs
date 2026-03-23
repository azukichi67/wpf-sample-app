using System.IO;
using ClosedXML.Excel;
using CommunityToolkit.Mvvm.Input;
using SampleApp.UI.ViewModels;

namespace SampleApp.UI.Tests.ViewModels;

public class ExcelToCsvViewModelTests
{
    [Fact]
    public void 初期値()
    {
        // Arrange, Act
        var vm = new ExcelToCsvViewModel();

        // Assert
        Assert.Equal(string.Empty, vm.ExcelFilePath);
        Assert.Equal(string.Empty, vm.CsvFilePath);
        Assert.Equal(string.Empty, vm.StatusMessage);
        Assert.False(vm.IsConverting);
    }

    public class ConvertCommand
    {
        [Fact]
        public void ExcelFilePathが空()
        {
            // Arrange
            var vm = new ExcelToCsvViewModel();
            vm.CsvFilePath = "output.csv";

            // Act & Assert
            Assert.False(vm.ConvertCommand.CanExecute(null));
        }

        [Fact]
        public void CsvFilePathが空()
        {
            // Arrange
            var vm = new ExcelToCsvViewModel();
            vm.ExcelFilePath = "input.xlsx";

            // Act & Assert
            Assert.False(vm.ConvertCommand.CanExecute(null));
        }

        [Fact]
        public void 両方設定済み()
        {
            // Arrange
            var vm = new ExcelToCsvViewModel();
            vm.ExcelFilePath = "input.xlsx";
            vm.CsvFilePath = "output.csv";

            // Act & Assert
            Assert.True(vm.ConvertCommand.CanExecute(null));
        }

        [Fact]
        public async Task 正常()
        {
            // Arrange
            using var excelFile = new TempFile(".xlsx");
            using var workbook = new XLWorkbook();
            IXLWorksheet sheet = workbook.AddWorksheet("Sheet1");
            sheet.Cell(1, 1).Value = "Name";
            sheet.Cell(1, 2).Value = "Alice";
            workbook.SaveAs(excelFile.Path);

            using var csvFile = new TempFile(".csv");
            var vm = new ExcelToCsvViewModel();
            vm.ExcelFilePath = excelFile.Path;
            vm.CsvFilePath = csvFile.Path;

            // Act
            await ((IAsyncRelayCommand)vm.ConvertCommand).ExecuteAsync(null);

            // Assert
            // 変換完了メッセージが表示され、変換中フラグが解除されること
            Assert.Equal("変換完了", vm.StatusMessage);
            Assert.False(vm.IsConverting);
        }
    }
}
