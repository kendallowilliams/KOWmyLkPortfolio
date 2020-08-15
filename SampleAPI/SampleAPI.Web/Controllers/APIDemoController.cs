﻿using SampleAPI.Web.Models;
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
    [Export(nameof(Pages.APIDemo), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIDemoController : BaseController
    {
        private readonly APIDemoViewModel apiDemoViewModel;

        [ImportingConstructor]
        public APIDemoController(APIDemoViewModel apiDemoViewModel) : base()
        {
            this.apiDemoViewModel = apiDemoViewModel;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View(apiDemoViewModel));
        }
    }
}