namespace SampleApp.Core.Services.Excel
{
    public class ExcelService
    {
        public void ConvertToCsv(CovertToCsvParam param)
        {
            var converter = new ExcelConverter(param.ExcelFilePath);
            converter.ToCsv(param.CsvFilePath);
        }
    }

    public record CovertToCsvParam
    {
        public required string ExcelFilePath { get; init; }
        public required string CsvFilePath { get; init; }
    }
}
