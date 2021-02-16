using DevTools.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.BLL.Services
{
    public class MefService : IMefService
    {
        private CompositionContainer container;
        private string pluginPath;
        private DirectoryCatalog catalog;

        public MefService(string pluginPath)
        {
            this.pluginPath = pluginPath;
            catalog = new DirectoryCatalog(pluginPath);
            container = new CompositionContainer(catalog);
        }

        public void Dispose()
        {
            container?.Dispose();
        }

        public Lazy<T> GetExport<T>()
        {
            return container.GetExport<T>();
        }

        public T GetExportedValue<T>()
        {
            return container.GetExportedValue<T>();
        }
    }
}
