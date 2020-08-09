using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;
using SampleAPI.DAL.Services.Interfaces;
using SampleAPI.DAL.DbContexts;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIProfile), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIProfileController : BaseController
    {
        private readonly APIProfileViewModel apiProfileViewModel;
        private readonly IDataService dataService;

        [ImportingConstructor]
        public APIProfileController(APIProfileViewModel apiProfileViewModel, IDataService dataService) 
        {
            this.apiProfileViewModel = apiProfileViewModel;
            this.dataService = dataService;
        }

        public async Task<ActionResult> Index()
        {
            apiProfileViewModel.APIProfiles = await dataService.GetList<APIProfile>();

            return View(apiProfileViewModel);
        }

        public async Task<ActionResult> APIProfile(int id)
        {
            APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == id);

            return PartialView("~/Views/APIProfile/APIProfile.cshtml", profile);
        }
    }
}