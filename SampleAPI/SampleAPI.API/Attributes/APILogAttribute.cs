using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleAPI.API.Attributes
{
    public class APILogAttribute : ActionFilterAttribute
    {
        public APILogAttribute() : base() { }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }
    }
}