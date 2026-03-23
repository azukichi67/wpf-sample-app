using System.IO;

namespace SampleApp.UI.Tests;

internal sealed class TempFile : IDisposable
{
    public string Path { get; }

    public TempFile(string extension = ".tmp")
    {
        Path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            Guid.NewGuid().ToString("N") + extension
        );
    }

    public void Dispose()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}
