using DevTools.WebUI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IViewModel viewModel;

        public BaseController(IViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
