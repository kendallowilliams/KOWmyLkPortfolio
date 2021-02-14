using DevTools.WebUI.MefHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.MefHelper
{
    public class MefFactory : IMefFactory
    {
        private CompositionContainer container;
        private string pluginPath;
        private DirectoryCatalog catalog;

        public MefFactory(string pluginPath)
        {
            this.pluginPath = pluginPath;
            catalog = new DirectoryCatalog(pluginPath);
            container = new CompositionContainer(catalog);
        }

        public T GetExportedValue<T>()
        {
            return container.GetExportedValue<T>();
        }
    }
}
