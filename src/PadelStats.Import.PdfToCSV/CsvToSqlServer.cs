using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadelStats.Import.PdfToCSV
{
    public class CsvToSqlServer
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public CsvToSqlServer(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        public void ImportCsvToSqlServer(string csvFilePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Countries", typeof(string));
            dt.Columns.Add("Points", typeof(int));
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Move", typeof(int));

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                // Skip the header row
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dt.Rows.Add(fields[0], fields[1], int.Parse(fields[2]), int.Parse(fields[3]), int.Parse(fields[4]));
                }
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = _tableName;
                    bulkCopy.BatchSize = 1000; // Adjust this value based on your needs

                    bulkCopy.ColumnMappings.Add("Title", "Title");
                    bulkCopy.ColumnMappings.Add("Countries", "Countries");
                    bulkCopy.ColumnMappings.Add("Points", "Points");
                    bulkCopy.ColumnMappings.Add("Position", "Position");
                    bulkCopy.ColumnMappings.Add("Move", "Move");

                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        Console.WriteLine($"Successfully imported {dt.Rows.Count} rows to {_tableName}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error importing data: {ex.Message}");
                    }
                }
            }
        }
    }
}
