using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIProfile), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIProfileController : BaseController
    {
        private readonly APIProfileViewModel apiProfileViewModel;

        [ImportingConstructor]
        public APIProfileController(APIProfileViewModel apiProfileViewModel) 
        {
            this.apiProfileViewModel = apiProfileViewModel;
        }

        public ActionResult Index()
        {
            return View(apiProfileViewModel);
        }
    }
}