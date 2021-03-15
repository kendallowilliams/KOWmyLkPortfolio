using DevTools.Shared.Models;
using DevTools.WebUI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Models
{
    public class SettingsViewModel : IViewModel
    {
        public SettingsViewModel()
        {
        }

        public DevToolsSettings DevToolsSettings { get; set; }
    }
}
