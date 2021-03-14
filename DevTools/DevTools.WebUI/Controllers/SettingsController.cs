using DevTools.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly SettingsViewModel settingsViewModel;

        public SettingsController(SettingsViewModel settingsViewModel) : base(settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
