// See https://aka.ms/new-console-template for more information
using PadelStats.Import.PdfToCSV;

Console.WriteLine("Hello, World!");


string pdfFilePath = @"C:\padel\Data\Ranking-Male-30-09-24.pdf";
string csvFilePath = @"C:\padel\Data\Ranking-Male-30-09-24.csv";

//PdfSplitter.ConvertPdfToCsv(pdfFilePath, csvFilePath);
Console.WriteLine("completed");

string connectionString = "Server=.;Database=PadelStats;User Id=sa;Password=123;";
string tableName = "MenWorldRanking";


var importer = new CsvToSqlServer(connectionString, tableName);
importer.ImportCsvToSqlServer(csvFilePath);