using GeoLog.Models;
using GeoLog.Services.Interfaces;
using System.IO;
using System.Text.Json;

namespace GeoLog.Services
{
    public class ProjectService : IProjectService
    {
        

        public void SaveToDatabase(string fileName, object newContent)
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(ExternalData.ProjectsDirectory))
                {
                    Directory.CreateDirectory(ExternalData.ProjectsDirectory);
                }

                // Ensure the file has a .json extension
                if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    fileName += ".json";
                }

                // Define the full file path
                string filePath = Path.Combine(ExternalData.ProjectsDirectory, fileName);

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

        public void AddBoreholeToProject(string boreholeNumber)
        {
            var isParsed = int.TryParse(boreholeNumber, out var parsed);

            if (!isParsed)
            {
                MessageBox.Show("Gręžinio pavadinimas privalo būti numeris");
                return;
            }

            var BoreholeName = $"Gr.{boreholeNumber}";

            var isExist = ProjectData.CurrentProject.Boreholes.Any(x => x.Name == BoreholeName);

            if (isExist)
            {
                MessageBox.Show("Gręžinys tokiu pavadinimu jau egzistuoja");
                return;
            }

            var createdBorehole = new Borehole(BoreholeName);

            ProjectData.CurrentProject.AddBorehole(createdBorehole);

            SaveToDatabase(ProjectData.CurrentProject.Name, ProjectData.CurrentProject);
        }

        public void UpdateBoreholeToProject(string oldBoreholeName, string newBoreholeName)
        {
            var boreholeOld = ProjectData.CurrentProject.Boreholes.FirstOrDefault(x => x.Name == oldBoreholeName);

            if (boreholeOld == null)
            {
                MessageBox.Show("Gręžinys nerastas");
                return;
            }

            var boreholeNew = ProjectData.CurrentProject.Boreholes.FirstOrDefault(x => x.Name == newBoreholeName);

            if (boreholeNew != null)
            {
                MessageBox.Show("Gręžinys tokiu pavadinimu jau egzistuoja");
                return;
            }

            boreholeOld.Name = $"{newBoreholeName}";
            ProjectData.CurrentProject.Boreholes = ProjectData.CurrentProject.Boreholes.OrderBy(x => x.Name).ToList();

            SaveToDatabase(ProjectData.CurrentProject.Name, ProjectData.CurrentProject);

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

    }
}
