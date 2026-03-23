using SampleApp.Core.Services.Excel;

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: SampleApp.CLI <target_folder>");
    return 1;
}

string targetFolder = args[0];

if (!Directory.Exists(targetFolder))
{
    Console.Error.WriteLine($"Folder not found: {targetFolder}");
    return 1;
}

string csvFolder = Path.Combine(targetFolder, "csv");
string succeedFolder = Path.Combine(targetFolder, "succeed");
string failedFolder = Path.Combine(targetFolder, "failed");

Directory.CreateDirectory(csvFolder);
Directory.CreateDirectory(succeedFolder);
Directory.CreateDirectory(failedFolder);

string[] xlsxFiles = Directory.GetFiles(targetFolder, "*.xlsx");

if (xlsxFiles.Length == 0)
{
    Console.WriteLine("No .xlsx files found.");
    return 0;
}

var service = new ExcelService();
int successCount = 0;
int failCount = 0;

foreach (string xlsxPath in xlsxFiles)
{
    string fileName = Path.GetFileNameWithoutExtension(xlsxPath);
    string csvPath = Path.Combine(csvFolder, fileName + ".csv");

    try
    {
        service.ConvertToCsv(new CovertToCsvParam
        {
            ExcelFilePath = xlsxPath,
            CsvFilePath = csvPath,
        });

        File.Move(xlsxPath, Path.Combine(succeedFolder, Path.GetFileName(xlsxPath)));
        Console.WriteLine($"[OK] {Path.GetFileName(xlsxPath)}");
        successCount++;
    }
    catch (Exception ex)
    {
        File.Move(xlsxPath, Path.Combine(failedFolder, Path.GetFileName(xlsxPath)));
        Console.Error.WriteLine($"[NG] {Path.GetFileName(xlsxPath)}: {ex.Message}");
        failCount++;
    }
}

Console.WriteLine($"Done. success={successCount}, failed={failCount}");
return failCount > 0 ? 1 : 0;
