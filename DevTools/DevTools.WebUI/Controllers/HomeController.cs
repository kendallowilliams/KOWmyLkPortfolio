using DevTools.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HomeViewModel homeViewModel;

        public HomeController(HomeViewModel homeViewModel) : base(homeViewModel)
        {
            this.homeViewModel = homeViewModel;
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
