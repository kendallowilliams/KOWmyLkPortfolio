using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIService), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIServiceController : BaseController
    {
        private readonly APIServiceViewModel apiServiceViewModel;

        [ImportingConstructor]
        public APIServiceController(APIServiceViewModel apiServiceViewModel) 
        {
            this.apiServiceViewModel = apiServiceViewModel;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View(apiServiceViewModel));
        }
    }
}