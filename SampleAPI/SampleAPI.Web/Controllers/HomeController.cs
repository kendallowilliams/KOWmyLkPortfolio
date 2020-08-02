using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleAPI.Web.Controllers
{
    [Export]
    public class HomeController : BaseController
    {
        [ImportingConstructor]
        public HomeController() { }

        public ActionResult Index()
        {
            return View();
        }
    }
}