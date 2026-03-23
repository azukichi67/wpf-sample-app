using System.IO;
using ClosedXML.Excel;
using SampleApp.Core.Services.Excel;

namespace SampleApp.Core.Tests.Services.Excel;

public class ExcelConverterTests
{
    private static TempFile CreateExcelFile(Action<IXLWorksheet> setupSheet)
    {
        var tempFile = new TempFile(".xlsx");
        using var workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.AddWorksheet("Sheet1");
        setupSheet(sheet);
        workbook.SaveAs(tempFile.Path);
        return tempFile;
    }

    public class ToCsv
    {
        [Fact]
        public void 正常()
        {
            // Arrange
            using TempFile excelFile = CreateExcelFile(sheet =>
            {
                sheet.Cell(1, 1).Value = "Name";
                sheet.Cell(1, 2).Value = "Age";
                sheet.Cell(2, 1).Value = "Alice";
                sheet.Cell(2, 2).Value = "30";
            });
            using var csvFile = new TempFile(".csv");
            var converter = new ExcelConverter(excelFile.Path);

            // Act
            converter.ToCsv(csvFile.Path);

            // Assert
            // ヘッダー行・データ行が正しく出力されること
            string[] lines = File.ReadAllLines(csvFile.Path);
            Assert.Equal("Name,Age", lines[0]);
            Assert.Equal("Alice,30", lines[1]);
        }

        [Fact]
        public void 空のシート()
        {
            // Arrange
            using TempFile excelFile = CreateExcelFile(_ => { });
            using var csvFile = new TempFile(".csv");
            var converter = new ExcelConverter(excelFile.Path);

            // Act
            converter.ToCsv(csvFile.Path);

            // Assert
            // 空のファイルが出力されること
            string content = File.ReadAllText(csvFile.Path);
            Assert.Equal("", content);
        }

        [Fact]
        public void カンマを含むセル()
        {
            // Arrange
            using TempFile excelFile = CreateExcelFile(sheet =>
            {
                sheet.Cell(1, 1).Value = "a,b";
                sheet.Cell(1, 2).Value = "c";
            });
            using var csvFile = new TempFile(".csv");
            var converter = new ExcelConverter(excelFile.Path);

            // Act
            converter.ToCsv(csvFile.Path);

            // Assert
            // カンマを含むセルはダブルクォートで囲まれること
            string[] lines = File.ReadAllLines(csvFile.Path);
            Assert.Equal("\"a,b\",c", lines[0]);
        }

        [Fact]
        public void ダブルクォートを含むセル()
        {
            // Arrange
            using TempFile excelFile = CreateExcelFile(sheet =>
            {
                sheet.Cell(1, 1).Value = "say \"hello\"";
                sheet.Cell(1, 2).Value = "c";
            });
            using var csvFile = new TempFile(".csv");
            var converter = new ExcelConverter(excelFile.Path);

            // Act
            converter.ToCsv(csvFile.Path);

            // Assert
            // ダブルクォートは二重にエスケープされること
            string[] lines = File.ReadAllLines(csvFile.Path);
            Assert.Equal("\"say \"\"hello\"\"\",c", lines[0]);
        }

        [Fact]
        public void 改行を含むセル()
        {
            // Arrange
            using TempFile excelFile = CreateExcelFile(sheet =>
            {
                sheet.Cell(1, 1).Value = "line1\nline2";
                sheet.Cell(1, 2).Value = "c";
            });
            using var csvFile = new TempFile(".csv");
            var converter = new ExcelConverter(excelFile.Path);

            // Act
            converter.ToCsv(csvFile.Path);

            // Assert
            // 改行を含むセルはダブルクォートで囲まれること
            string content = File.ReadAllText(csvFile.Path);
            Assert.StartsWith("\"line1\nline2\",c", content);
        }
    }
}
