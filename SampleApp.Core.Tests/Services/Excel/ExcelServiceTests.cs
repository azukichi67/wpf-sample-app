using System.IO;
using ClosedXML.Excel;
using SampleApp.Core.Services.Excel;

namespace SampleApp.Core.Tests.Services.Excel;

public class ExcelServiceTests
{
    public class ConvertToCsv
    {
        [Fact]
        public void 正常()
        {
            // Arrange
            using var excelFile = new TempFile(".xlsx");
            using var workbook = new XLWorkbook();
            IXLWorksheet sheet = workbook.AddWorksheet("Sheet1");
            sheet.Cell(1, 1).Value = "Name";
            sheet.Cell(1, 2).Value = "Age";
            sheet.Cell(2, 1).Value = "Alice";
            sheet.Cell(2, 2).Value = "30";
            workbook.SaveAs(excelFile.Path);

            using var csvFile = new TempFile(".csv");
            var service = new ExcelService();

            // Act
            service.ConvertToCsv(new CovertToCsvParam
            {
                ExcelFilePath = excelFile.Path,
                CsvFilePath = csvFile.Path,
            });

            // Assert
            // ヘッダー行・データ行が正しく出力されること
            string[] lines = File.ReadAllLines(csvFile.Path);
            Assert.Equal("Name,Age", lines[0]);
            Assert.Equal("Alice,30", lines[1]);
        }
    }
}
