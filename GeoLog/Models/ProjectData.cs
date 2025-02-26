using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLog.Models
{
    public static class ProjectData
    {
        private static Project _currentProject;
        public static event Action<Project> OnProjectChanged;

        public static Project CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnProjectChanged?.Invoke(_currentProject); // Notify all UI components
            }
        }
    }
}
