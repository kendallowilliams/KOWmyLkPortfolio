using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.Home), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : BaseController
    {
        private readonly HomeViewModel homeViewModel;

        [ImportingConstructor]
        public HomeController(HomeViewModel homeViewModel)
        {
            this.homeViewModel = homeViewModel;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View(homeViewModel));
        }
    }
}