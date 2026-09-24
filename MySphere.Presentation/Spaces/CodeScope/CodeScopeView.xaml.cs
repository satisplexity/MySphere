using System.IO;
using System.Windows.Controls;

namespace MySphere.Presentation.Spaces.CodeScope;

public partial class CodeScopeView : UserControl
{
    public CodeScopeView()
    {
        InitializeComponent();

        Calculate();
    }

    private void Calculate()
    {
        string rootPath = @"P:\MySphere";

        var stats = new
        {
            Cs = new FileStats(),
            Xaml = new FileStats()
        };

        var ignoredDirectories = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            "bin",
            "obj"
        };

        foreach (var file in Directory.EnumerateFiles(
                     rootPath,
                     "*.*",
                     SearchOption.AllDirectories))
        {
            // Игнорируем bin и obj
            if (file.Split(Path.DirectorySeparatorChar)
                    .Any(x => ignoredDirectories.Contains(x)))
            {
                continue;
            }

            switch (Path.GetExtension(file).ToLowerInvariant())
            {
                case ".cs":
                    ProcessFile(file, stats.Cs);
                    break;

                case ".xaml":
                    ProcessFile(file, stats.Xaml);
                    break;
            }
        }

        PrintStats(".cs", stats.Cs);
        PrintStats(".xaml", stats.Xaml);

        PART_ReportText.Text += $"TOTAL\n";
        PART_ReportText.Text += $"Files:      {stats.Cs.Files + stats.Xaml.Files:N0}\n";
        PART_ReportText.Text += $"Lines:      {stats.Cs.Lines + stats.Xaml.Lines:N0}\n";
        PART_ReportText.Text += $"Characters: {stats.Cs.Characters + stats.Xaml.Characters:N0}";
    }

    private void ProcessFile(string path, FileStats stats)
    {
        string text = File.ReadAllText(path);

        stats.Files++;
        stats.Lines += CountLines(text);
        stats.Characters += text.Length;
    }

    private int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        return text.Count(c => c == '\n');
    }

    private void PrintStats(string extension, FileStats stats)
    {
        PART_ReportText.Text += $"{extension}\n";
        PART_ReportText.Text += $"Files:      {stats.Files:N0}\n";
        PART_ReportText.Text += $"Lines:      {stats.Lines:N0}\n";
        PART_ReportText.Text += $"Characters: {stats.Characters:N0}\n\n";
    }
}

internal class FileStats
{
    public int Files { get; set; }
    public long Lines { get; set; }
    public long Characters { get; set; }
}