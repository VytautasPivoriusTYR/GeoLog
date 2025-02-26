using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLog
{
    public class ExternalData
    {
        public static string local = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string ProjectsDirectory = @$"{local}\GeoLogProjects";
    }
}
