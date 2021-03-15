using DevTools.Shared.Models;
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
using static DevTools.Shared.Enums;

namespace DevTools.BLL.Services
{
    [Export(typeof(IProcessorService))]
    public class ProcessorService : IProcessorService
    {
        private readonly IDataService dataService;
        private readonly IConsoleService consoleService;
        private readonly string localPath;

        [ImportingConstructor]
        public ProcessorService(IDataService dataService, IConsoleService consoleService)
        {
            this.dataService = dataService;
            this.consoleService = consoleService;
            localPath = WorkingDirectory;
        }

        public static string WorkingDirectory { get => Path.Combine(Path.GetTempPath(), "DevTools"); }

        public async Task ProcessScaffoldDbContextItem(DevToolsProcessorItem item)
        {
            Guid profileId = item.GetRequest<Guid>();
            ScaffoldDbContextProfile profile = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectId == profileId &&
                                                                                                               item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile))
                                                                .ContinueWith(task => task.Result?.GetObject<ScaffoldDbContextProfile>());
            DevToolsSettings settings = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectType == nameof(DevToolsObjectType))
                                                         .ContinueWith(task => task.Result?.GetObject<DevToolsSettings>() ?? new DevToolsSettings());
            string outputPath = Path.Combine(localPath, "Output"),
                   safeProfileName = new string(profile.Name.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch).ToArray()),
                   dateTimeFormat = "yyyyMMdd_HHmmss",
                   outputFile = Path.Combine(outputPath, $"{safeProfileName}_{DateTime.Now.ToString(dateTimeFormat)}.zip");
            StringBuilder builder = new StringBuilder();

            profile.ScaffoldDbContextConfig.Project = Path.Combine(localPath, profile.ScaffoldDbContextConfig.Project);
            profile.ScaffoldDbContextConfig.StartupProject = Path.Combine(localPath, profile.ScaffoldDbContextConfig.StartupProject);
            builder.AppendLine(consoleService.Execute(settings.DotNetExe, profile.GetProjectCleanArguments()));
            builder.AppendLine(consoleService.Execute(settings.DotNetExe, profile.GetStartupProjectCleanArguments()));
            builder.AppendLine(consoleService.Execute(settings.DotNetExe, $"ef dbcontext scaffold {profile.ScaffoldDbContextConfig.BuildArgumentList(profile.BuildConfiguration)}"));
            builder.AppendLine();
            builder.AppendLine(consoleService.Execute(settings.DotNetExe, profile.GetProjectBuildArguments(outputPath)));
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

        public async Task ProcessScaffoldDbContextItems()
        {
            IEnumerable<DevToolsProcessorItem> items = await dataService.GetList<DevToolsEntities, DevToolsProcessorItem>(item => item.Status == nameof(Status.Created) && 
                                                                                                                                  item.RequestType == nameof(ProcessorItemType.ScaffoldDbContextProfile));
            foreach(var item in items)
            {
                try
                {
                    await ProcessScaffoldDbContextItem(item);
                }
                catch (Exception ex)
                {
                    item.ModifiedBy = "SYSTEM";
                    item.EndedOn = item.ModifiedOn = DateTime.Now;
                    item.IsError = true;
                    item.Message = ex.Message;
                    await dataService.Update<DevToolsEntities, DevToolsProcessorItem>(item);
                }
            }
        }

        public void CleanDirectory(string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            if (dirInfo.Exists)
            {
                foreach(var fileInfo in dirInfo.GetFiles())
                {
                    fileInfo.Delete();
                }

                foreach (var subDirInfo in dirInfo.GetDirectories())
                {
                    CleanDirectory(directory);
                    subDirInfo.Delete(true);
                }
            }
        }
    }
}
