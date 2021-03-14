using DevTools.BLL.Services;
using DevTools.BLL.Services.Interfaces;
using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DevTools.Console
{
    class Program
    {
        public Program()
        {
        }

        static async Task Main(string[] args)
        {
            using (IMefService mefService = new MefService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                IProcessorService processorService = mefService.GetExportedValue<IProcessorService>();

                await processorService.ProcessScaffoldDbContextItems();
            }
        }
    }
}
