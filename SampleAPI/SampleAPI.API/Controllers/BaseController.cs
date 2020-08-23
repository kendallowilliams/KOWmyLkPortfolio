using SampleAPI.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleAPI.API.Controllers
{
    [Authentication]
    [Authorization]
    [APILog]
    public abstract class BaseController : ApiController
    {
        public BaseController() { }
    }
}
