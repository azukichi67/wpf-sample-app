using ClosedXML.Excel;

namespace SampleApp.Core.Services.Excel
{
    public class ExcelConverter
    {
        private readonly string _srcPath;
        public ExcelConverter(string srcPath)
        {
            _srcPath = srcPath;
        }

        /// <summary>
        /// CSVファイルに変換して保存します
        /// </summary>
        /// <param name="destPath">出力先パス</param>
        public void ToCsv(string destPath)
        {
            using var workbook = new XLWorkbook(_srcPath);
            IXLWorksheet sheet = workbook.Worksheet(1);
            IXLRangeRows? rows = sheet.RangeUsed()?.RowsUsed();

            using var writer = new StreamWriter(destPath, append: false, encoding: System.Text.Encoding.UTF8);

            if (rows is null)
            {
                writer.Write("");
                return;
            }

            foreach (IXLRangeRow? row in rows)
            {
                IEnumerable<string> cells = row.Cells().Select(c => EscapeCsvField(c.GetString()));
                writer.WriteLine(string.Join(",", cells));
            }
        }

        private string EscapeCsvField(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }
    }
}
