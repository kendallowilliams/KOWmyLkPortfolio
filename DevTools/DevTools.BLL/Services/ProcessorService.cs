using DevTools.BLL.Models;
using DevTools.BLL.Services.Interfaces;
using DevTools.DAL.DbContexts;
using DevTools.DAL.Models;
using DevTools.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevTools.DAL.Enums;

namespace DevTools.BLL.Services
{
    [Export(typeof(IProcessorService))]
    public class ProcessorService : IProcessorService
    {
        private readonly IDataService dataService;
        private readonly IConsoleService consoleService;

        [ImportingConstructor]
        public ProcessorService(IDataService dataService, IConsoleService consoleService)
        {
            this.dataService = dataService;
            this.consoleService = consoleService;
        }

        public async Task ProcessScaffoldDbContextItem(Guid id)
        {
            ScaffoldDbContextProfile profile = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectId == id &&
                                                                                                               item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile))
                                                                .ContinueWith(task => task.Result?.GetObject<ScaffoldDbContextProfile>());
            string tempFolder = Path.GetTempPath(),
                    localPath = Path.Combine(tempFolder, Guid.NewGuid().ToString("N")),
                    outputPath = Path.Combine(localPath, "Output"),
                    safeProfileName = new string(profile.Name.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch).ToArray()),
                    dateTimeFormat = "yyyyMMdd_HHmmss",
                    outputFile = Path.Combine(outputPath, $"{safeProfileName}_{DateTime.Now.ToString(dateTimeFormat)}.zip");
            StringBuilder builder = new StringBuilder();

            Directory.CreateDirectory(localPath);
            profile.ScaffoldDbContextConfig.Project = Path.Combine(localPath, profile.ScaffoldDbContextConfig.Project);
            profile.ScaffoldDbContextConfig.StartupProject = Path.Combine(localPath, profile.ScaffoldDbContextConfig.StartupProject);
            builder.AppendLine(consoleService.Execute("dotnet", profile.GetProjectCleanArguments()));
            builder.AppendLine(consoleService.Execute("dotnet", profile.GetStartupProjectCleanArguments()));
            builder.AppendLine(consoleService.Execute("dotnet", $"ef dbcontext scaffold {profile.ScaffoldDbContextConfig.BuildArgumentList(profile.BuildConfiguration)}"));
            builder.AppendLine();
            builder.AppendLine(consoleService.Execute("dotnet", profile.GetProjectBuildArguments(outputPath)));
            System.IO.File.WriteAllText(Path.Combine(outputPath, "log.txt"), builder.ToString());

            using (var memoryStream = new MemoryStream())
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (string file in Directory.EnumerateFiles(outputPath))
                    {
                        ziparchive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }

                System.IO.File.WriteAllBytes(outputFile, memoryStream.ToArray());
            }
        }

        public async Task ProcessScaffoldDbContexts()
        {
        }
    }
}
