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
        private static Borehole _currentBorehole;
        private static BoreholeLayer _currentBoreholeLayer;

        public static event Action<Project> OnProjectChanged;
        public static event Action<Borehole> OnBoreholeChanged;
        public static event Action<BoreholeLayer> OnBoreholeLayerChanged;

        public static Project CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnProjectChanged?.Invoke(_currentProject); // Notify all UI components
            }
        }

        public static Borehole CurrentBorehole
        {
            get => _currentBorehole;
            set
            {
                _currentBorehole = value;
                OnBoreholeChanged?.Invoke(_currentBorehole);
            }
        }

        public static BoreholeLayer CurrentBoreholeLayer
        {
            get => _currentBoreholeLayer;
            set
            {
                _currentBoreholeLayer = value;
                OnBoreholeLayerChanged?.Invoke(_currentBoreholeLayer);
            }
        }
    }
}
