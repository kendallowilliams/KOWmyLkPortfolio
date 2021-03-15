using DevTools.BLL.Services.Interfaces;
using DevTools.DAL.DbContexts;
using DevTools.DAL.Models;
using DevTools.DAL.Services.Interfaces;
using DevTools.Shared.Models;
using DevTools.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static DevTools.Shared.Enums;

namespace DevTools.WebUI.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly SettingsViewModel settingsViewModel;
        private readonly IDataService dataService;

        public SettingsController(SettingsViewModel settingsViewModel, IMefService mefService) : base(settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
            this.dataService = mefService.GetExportedValue<IDataService>();
        }

        public async Task<IActionResult> Index()
        {
            settingsViewModel.DevToolsSettings = await dataService.Get<DevToolsEntities, DevToolsObject>(item => item.ObjectType == nameof(DevToolsObjectType.DevToolsSettings))
                                                                  .ContinueWith(task => task.Result?.GetObject<DevToolsSettings>() ?? new DevToolsSettings());

            return View(settingsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
