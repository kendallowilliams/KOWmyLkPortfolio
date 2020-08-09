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
    [Export(nameof(Pages.APIService), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIServiceController : BaseController
    {
        private readonly APIServiceViewModel apiServiceViewModel;
        private readonly IDataService dataService;

        [ImportingConstructor]
        public APIServiceController(APIServiceViewModel apiServiceViewModel, IDataService dataService) 
        {
            this.apiServiceViewModel = apiServiceViewModel;
            this.dataService = dataService;
        }

        public async Task<ActionResult> Index()
        {
            apiServiceViewModel.APIServices = await dataService.GetList<APIService>();

            return View(apiServiceViewModel);
        }

        public async Task<ActionResult> APIService(int id)
        {
            APIService Service = await dataService.Get<APIService>(item => item.Id == id);

            return PartialView("~/Views/APIService/APIService.cshtml", Service);
        }
    }
}