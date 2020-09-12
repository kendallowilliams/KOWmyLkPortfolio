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
        protected readonly Uri apiUri;

        public BaseController()
        {
            APIAddress = WebConfigurationManager.AppSettings[nameof(APIAddress)];
            apiUri = new Uri(WebConfigurationManager.AppSettings["APIAddress"]);
        }

        public string APIAddress { get; private set; }

        public static bool IsDemo()
        {
#if DEMO
            return true;
#else
            return false;
#endif
        }
    }
}