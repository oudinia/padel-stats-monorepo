using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using QuestPDF.Fluent;
using System.Text;
using System.Text.RegularExpressions;

namespace PadelStats.Import.PdfToCSV
{
    public static class PdfSplitter
    {
        public static void ConvertPdfToCsv(string pdfPath, string csvPath)
        {
            using (var reader = new PdfReader(pdfPath))
            using (var document = new PdfDocument(reader))
            using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
            {
                bool isFirstPage = true;

                for (int i = 1; i <= document.GetNumberOfPages(); i++)
                {
                    var page = document.GetPage(i);
                    var strategy = new SimpleTextExtractionStrategy();
                    var currentText = PdfTextExtractor.GetTextFromPage(page, strategy);

                    var lines = currentText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                                           .Select(line => line.Trim())
                                           .Where(line => !string.IsNullOrWhiteSpace(line))
                                           .ToList();

                    if (isFirstPage)
                    {
                        // Write header
                        writer.WriteLine("Title,Countries,Points,Position,Move");
                        isFirstPage = false;
                    }

                    // Write data rows
                    foreach (var line in lines)
                    {
                        var formattedLine = FormatCsvLine(line);
                        if (!string.IsNullOrEmpty(formattedLine))
                        {
                            writer.WriteLine(formattedLine);
                        }
                    }
                }
            }
        }

        public static string FormatCsvLine(string line)
        {
            // Use regex to match the pattern with any three-letter country code
            var match = Regex.Match(line, @"^(.*?)\s+([A-Z]{3})\s+(\d+)\s+(\d+)\s+(\d+)$");

            if (match.Success)
            {
                string title = match.Groups[1].Value.Trim();
                string country = match.Groups[2].Value;
                string points = match.Groups[3].Value;
                string position = match.Groups[4].Value;
                string move = match.Groups[5].Value;

                // Return CSV formatted line with Title and Countries separated
                return $"\"{title}\",{country},{points},{position},{move}";
            }

            // If the line doesn't match the expected format, return null
            return null;
        }

        // Example method using QuestPDF for future PDF generation
        public static void CreateSamplePdf(string outputPath)
        {
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().PaddingVertical(50).Column(column =>
                    {
                        column.Item().Text("Sample PDF Generated with QuestPDF").FontSize(20);
                        column.Item().Text("This is a placeholder for future PDF generation tasks.").FontSize(14);
                    });
                });
            })
            .GeneratePdf(outputPath);

            Console.WriteLine($"Sample PDF created: {outputPath}");
        }
    }
}
