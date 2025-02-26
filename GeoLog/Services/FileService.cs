using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Text.Json;

namespace GeoLog.Services
{
    public class FileService
    {
        /// <summary>
        /// Leidžia vartotojui pasirinkti excel failą
        /// </summary>
        /// <returns>Gaunama pilna nuoroda į excel failą</returns>
        public string SelectExcelFileAndGetPath()
        {
            try
            {
                string filename = "";

                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.FileName = ""; // Default file name
                dialog.DefaultExt = ".xlsx"; // Default file extension
                dialog.Filter = "Excel documents (.xlsx)|*.xlsx"; // Filter files by extension
                dialog.Multiselect = false;

                // Show open file dialog box
                bool? result = dialog.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    filename = dialog.FileName;
                }
                return filename;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Leidžia vartotojui pasirinkti aplanką.
        /// </summary>
        /// <returns>Gaunama aplanko pilna nuoroda</returns>
        public string SelectFolderAndGetPath()
        {
            string folderPath = "";

            var dialog = new Microsoft.Win32.OpenFolderDialog();

            // Show the folder browser dialog
            var result = dialog.ShowDialog();

            // Process dialog result
            if (result == true && !string.IsNullOrWhiteSpace(dialog.FolderName))
            {
                // Get the selected folder path
                folderPath = dialog.FolderName;
            }

            return folderPath;
        }

        /// <summary>
        /// Perskaitomas visas excel failas ir gaunami duomenys
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExt"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public System.Data.DataTable ReadExcel(string fileName, string fileExt, string sheetName)
        {
            string conn = string.Empty;
            System.Data.DataTable dtexcel = new System.Data.DataTable();
            if (fileExt.CompareTo(".xls") == 0) conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007
            else conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter($"select * from [{sheetName}$]", con); //here we read data from sheet1
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch { }
            }
            return dtexcel;

        }

        /// <summary>
        /// Perskaitomas visas excel failas ir gaunami duomenys kaip List
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<List<string>> ReadExcel(string filePath, string sheetName)
        {
            List<List<string>> data = new List<List<string>>();

            // Create an Excel application instance
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Open the Excel workbook
            Workbook workbook = excelApp.Workbooks.Open(filePath);

            // Get the specified sheet by name
            Worksheet worksheet = (Worksheet)workbook.Sheets[sheetName];

            // Get the used range of the sheet
            Microsoft.Office.Interop.Excel.Range usedRange = worksheet.UsedRange;

            // Read data from each cell in the used range
            object[,] excelArray = (object[,])usedRange.Value2;
            int rowCount = excelArray.GetLength(0);
            int colCount = excelArray.GetLength(1);

            for (int i = 1; i <= rowCount; i++)
            {
                List<string> rowData = new List<string>();
                for (int j = 1; j <= colCount; j++)
                {
                    // Convert Excel data to string and add to the list
                    rowData.Add(Convert.ToString(excelArray[i, j]));
                }
                data.Add(rowData);
            }

            // Close the workbook and release resources
            workbook.Close(false);
            excelApp.Quit();

            // Release COM objects to avoid memory leaks
            ReleaseObject(worksheet);
            ReleaseObject(usedRange);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);

            return data;
        }

        /// <summary>
        /// Gaunami excel failo visi sheet pavadinimai
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        public List<string> GetExcelSheets(string excelFilePath)
        {
            try
            {
                List<string> sheetNames = new List<string>();

                // Create an Excel application instance
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                // Open the Excel workbook
                Workbook workbook = excelApp.Workbooks.Open(excelFilePath);

                // Iterate through all sheets and add their names to the list
                foreach (Worksheet sheet in workbook.Sheets)
                {
                    sheetNames.Add(sheet.Name);
                }

                // Close the workbook and release resources
                workbook.Close(false);
                excelApp.Quit();

                // Release COM objects to avoid memory leaks
                ReleaseObject(workbook);
                ReleaseObject(excelApp);
                return sheetNames;
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        /// <summary>
        /// Atlaisvinamas excel failas po nuskaitymo. Konfiguracinis metodas
        /// </summary>
        /// <param name="obj"></param>
        static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Patikrina ar failas egzistuoja direktorijoje
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public bool IsfileExist(string filePath)
        {
            try
            {
                // Delete the ZIP file after extraction
                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public void CreateExcelFile(string directoryPath, string fileName)
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Define the full file path
                string filePath = Path.Combine(directoryPath, fileName);

                // Create Excel application instance
                Excel.Application excelApp = new Excel.Application
                {
                    Visible = false, // Run Excel in the background
                    DisplayAlerts = false
                };

                // Create a new workbook
                Workbook workbook = excelApp.Workbooks.Add();
                Worksheet worksheet = (Worksheet)workbook.Sheets[1];

                // Add sample data
                worksheet.Cells[1, 1] = "Hello";
                worksheet.Cells[1, 2] = "World";

                // Save the workbook
                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();

                // Release COM objects to prevent memory leaks
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                Console.WriteLine($"Excel file created: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Excel file: {ex.Message}");
            }
        }

        public void CreateJsonFile(string directoryPath, string fileName, object jsonContent)
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Ensure the file name has the .json extension
                if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    fileName += ".json";
                }

                // Define the full file path
                string filePath = Path.Combine(directoryPath, fileName);

                // Serialize the object to JSON format
                string jsonString = JsonSerializer.Serialize(jsonContent, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON data to file
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"JSON file created: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating JSON file: {ex.Message}");
            }
        }

        public void UpdateJsonFile(string directoryPath, string fileName, object newContent)
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Define the full file path
                string filePath = Path.Combine(directoryPath, fileName);

                // Serialize the new object to JSON format
                string jsonString = JsonSerializer.Serialize(newContent, new JsonSerializerOptions { WriteIndented = true });

                // Overwrite the existing JSON file with new content
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"JSON file updated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating JSON file: {ex.Message}");
            }
        }

        public T ReadJsonFile<T>(string directoryPath, string fileName)
        {
            try
            {
                // Define the full file path
                string filePath = Path.Combine(directoryPath, fileName);

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found!");
                    return default; // Return default value for the type (null for reference types)
                }

                // Read JSON content
                string jsonString = File.ReadAllText(filePath);

                // Deserialize JSON to object
                T result = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"JSON file read successfully: {filePath}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return default;
            }
        }

        public List<T> ReadAllJsonFiles<T>(string directoryPath)
        {
            List<T> results = new List<T>();

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Directory not found!");
                    return results; // Return an empty list if directory doesn't exist
                }

                // Get all JSON files from the directory
                string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    try
                    {
                        // Read JSON content
                        string jsonString = File.ReadAllText(filePath);

                        // Deserialize JSON to object
                        T result = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (result != null)
                        {
                            results.Add(result);
                            Console.WriteLine($"Successfully read: {filePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON files from directory: {ex.Message}");
            }

            return results;
        }

    }
}
