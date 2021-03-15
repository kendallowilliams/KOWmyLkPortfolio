using DevTools.Shared.Models;
using DevTools.WebUI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Models
{
    public class ScaffoldDbContextProfilesViewModel : IViewModel
    {
        public ScaffoldDbContextProfilesViewModel()
        {
        }

        public IEnumerable<ScaffoldDbContextProfile> ScaffoldDbContextProfiles { get; set; }

        public Guid? SelectedProfileId { get; set; }
    }
}
