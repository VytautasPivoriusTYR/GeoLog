using GeoLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLog.Services.Interfaces
{
    public interface IProjectService
    {
        void SaveToDatabase(string fileName, object newContent);
        void AddBoreholeToProject(string boreholeNumber);
        void UpdateBoreholeToProject(string oldBoreholeName, string newBoreholeName);
        void CreateJsonFile(string directoryPath, string fileName, object jsonContent);
        List<T> ReadAllJsonFiles<T>(string directoryPath);

    }
}
