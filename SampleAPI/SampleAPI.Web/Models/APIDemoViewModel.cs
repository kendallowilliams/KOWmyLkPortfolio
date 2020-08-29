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
    public class APIDemoViewModel : BaseViewModel
    {
        [ImportingConstructor]
        public APIDemoViewModel() : base(Pages.APIDemo)
        {
            APIProfileServices = Enumerable.Empty<APIProfileService>();
            APIServices = Enumerable.Empty<APIService>();
        }

        public IEnumerable<APIProfileService> APIProfileServices { get; set; }
        public IEnumerable<APIService> APIServices { get; set; }
    }
}