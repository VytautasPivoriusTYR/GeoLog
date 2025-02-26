using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeoLog
{
    class Locator
    {
        private string _path;

        public Locator()
        {

        }

        public void InstallLocator(string path)
        {
            _path = path;
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
        }

        private Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            int position = args.RequestingAssembly.FullName.IndexOf(",");
            if (position > -1)
            {
                try
                {
                    string assemblyName = args.RequestingAssembly.FullName.Substring(0, position);
                    string assemblyFullPath = string.Empty;

                    //look in main folder
                    assemblyFullPath = _path + "\\" + assemblyName + ".dll";
                    if (File.Exists(assemblyFullPath))
                        return Assembly.LoadFrom(assemblyFullPath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
