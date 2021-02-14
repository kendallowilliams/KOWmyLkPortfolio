using DevTools.BLL.Models;
using DevTools.BLL.Services.Interfaces;
using DevTools.DAL.DbContexts;
using DevTools.DAL.Models;
using DevTools.DAL.Services.Interfaces;
using DevTools.WebUI.MefHelper.Interfaces;
using DevTools.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevTools.DAL.Enums;

namespace DevTools.WebUI.Controllers
{
    public class ScaffoldDbContextProfilesController : BaseController
    {
        private readonly ScaffoldDbContextProfilesViewModel scaffoldDbContextProfilesViewModel;
        private readonly IDataService dataService;
        private readonly IConsoleService consoleService;

        public ScaffoldDbContextProfilesController(ScaffoldDbContextProfilesViewModel scaffoldDbContextProfilesViewModel, IMefFactory mefFactory) : base(scaffoldDbContextProfilesViewModel)
        {
            this.scaffoldDbContextProfilesViewModel = scaffoldDbContextProfilesViewModel;
            consoleService = mefFactory.GetExportedValue<IConsoleService>();
            dataService = mefFactory.GetExportedValue<IDataService>();
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            scaffoldDbContextProfilesViewModel.ScaffoldDbContextProfiles = await dataService.GetList<DevToolsEntities, DevToolsObject>(item => item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile))
                                                                                            .ContinueWith(task => task.Result.Select(item => item.GetObject<ScaffoldDbContextProfile>()));
            scaffoldDbContextProfilesViewModel.SelectedProfileId = id;

            return View(scaffoldDbContextProfilesViewModel);
        }

        public async Task<ActionResult> ScaffoldDbContextProfile(Guid? id)
        {
            ScaffoldDbContextProfile profile = await dataService.Get<DevToolsEntities, DevToolsObject>(item => id.HasValue && item.ObjectId == id.Value &&
                                                                                                               item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile))
                                                                .ContinueWith(task => task.Result?.GetObject<ScaffoldDbContextProfile>());

            return PartialView(profile);
        }

        public async Task<Guid?> AddScaffoldDbContextProfile(string name)
        {
            Guid? id = null;

            if (!string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<ScaffoldDbContextProfile> profiles = await dataService.GetList<DevToolsEntities, DevToolsObject>(item => item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile))
                                                                                  .ContinueWith(task => task.Result.Select(item => item.GetObject<ScaffoldDbContextProfile>()));

                if (!profiles.Any(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    ScaffoldDbContextProfile profile = new ScaffoldDbContextProfile() { Name = name };
                    DevToolsObject dbProfile = new DevToolsObject()
                    {
                        ObjectId = profile.Id,
                        ObjectType = nameof(DevToolsObjectType.ScaffoldDbContextProfile),
                        ObjectJson = JsonConvert.SerializeObject(profile),
                        CreatedBy = HttpContext.Connection.RemoteIpAddress.ToString(),
                        ModifiedBy = HttpContext.Connection.RemoteIpAddress.ToString()
                    };

                    await dataService.Insert<DevToolsEntities, DevToolsObject>(dbProfile);
                    id = dbProfile.ObjectId;
                }
            }

            return id;
        }

        public async Task<ActionResult> UpdateScaffoldDbContextProfile(ScaffoldDbContextProfile scaffoldDbContextProfile)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<string> projectFiles = Directory.EnumerateFiles(scaffoldDbContextProfile.ProjectLocation),
                                    startupProjectFiles = Directory.EnumerateFiles(scaffoldDbContextProfile.StartupProjectLocation);
                string projectFile = projectFiles.FirstOrDefault(file => Path.GetExtension(file).Equals(".csproj", StringComparison.OrdinalIgnoreCase)),
                       startupProfileFile = startupProjectFiles.FirstOrDefault(file => Path.GetExtension(file).Equals(".csproj", StringComparison.OrdinalIgnoreCase));
                DevToolsObject profile = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectId == scaffoldDbContextProfile.Id &&
                                                                                                         item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile));

                scaffoldDbContextProfile.ScaffoldDbContextConfig.Tables = scaffoldDbContextProfile.ScaffoldDbContextConfig.Tables.Distinct(StringComparer.OrdinalIgnoreCase)
                                                                                                                                 .OrderBy(item => item, StringComparer.OrdinalIgnoreCase);
                scaffoldDbContextProfile.ScaffoldDbContextConfig.Project = projectFile;
                scaffoldDbContextProfile.ScaffoldDbContextConfig.StartupProject = startupProfileFile;
                profile.ObjectJson = JsonConvert.SerializeObject(scaffoldDbContextProfile);
                profile.ModifiedBy = HttpContext.Connection.RemoteIpAddress.ToString();
                profile.ModifiedOn = DateTime.Now;
                await dataService.Update<DevToolsEntities, DevToolsObject>(profile);
                ModelState.Clear();
            }

            return PartialView(nameof(ScaffoldDbContextProfile), scaffoldDbContextProfile);
        }

        public async Task DeleteScaffoldDbContextProfile(Guid id)
        {
            DevToolsObject profile = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectId == id &&
                                                                                                     item.ObjectType == nameof(DevToolsObjectType.ScaffoldDbContextProfile));

            if (profile != null) /*then*/ await dataService.Delete<DevToolsEntities, DevToolsObject>(profile.Id);
        }

        public async Task<IActionResult> Process(Guid id)
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
            builder.AppendLine(consoleService.Execute("dotnet", $"ef dbcontext scaffold {profile.ScaffoldDbContextConfig.BuildArgumentList(profile.BuildConfiguration, profile.Verbose)}"));
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

            return PhysicalFile(outputFile, "application/zip", Path.GetFileName(outputFile));
        }
    }
}
