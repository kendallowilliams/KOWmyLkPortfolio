using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Models
{
    [Export]
    public class APIProfileViewModel : BaseViewModel
    {
        [ImportingConstructor]
        public APIProfileViewModel() : base(Pages.APIProfile)
        {
            APIProfiles = Enumerable.Empty<APIProfile>();
        }

        public IEnumerable<APIProfile> APIProfiles { get; set; }
        public int? SelectedProfileId { get; set; }
    }
}