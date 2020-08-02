﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleAPI.Web.Controllers
{
    [Export]
    public class APIProfileController : BaseController
    {
        [ImportingConstructor]
        public APIProfileController() { }

        public ActionResult Index()
        {
            return View();
        }
    }
}