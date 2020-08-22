using SampleAPI.Web.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SampleAPI.Web.Controllers
{
    public abstract class BaseController : Controller, IBaseController
    {
        public BaseController()
        {
            APIAddress = WebConfigurationManager.AppSettings[nameof(APIAddress)];
        }

        public string APIAddress { get; private set; }
    }
}