using System;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelFilePath = "example.xlsx";
            string jsonFilePath = "example.json";

            // Load Excel file
            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Convert worksheet to object array
                object[,] worksheetArray = worksheet.Cells.Value as object[,];

                // Convert object array to array of strings
                string[,] stringArray = new string[worksheetArray.GetLength(0), worksheetArray.GetLength(1)];
                for (int i = 0; i < worksheetArray.GetLength(0); i++)
                {
                    for (int j = 0; j < worksheetArray.GetLength(1); j++)
                    {
                        stringArray[i, j] = worksheetArray[i + 1, j + 1]?.ToString();
                    }
                }

                // Convert array of strings to JSON
                string json = JsonConvert.SerializeObject(stringArray);

                // Write JSON to file
                File.WriteAllText(jsonFilePath, json);
            }
        }
    }
}
