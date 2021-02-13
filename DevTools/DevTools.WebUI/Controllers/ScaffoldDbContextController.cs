using DevTools.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Controllers
{
    public class ScaffoldDbContextController : BaseController
    {
        private readonly ScaffoldDbContextViewModel scaffoldDbContextViewModel;

        public ScaffoldDbContextController() : base(null)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
