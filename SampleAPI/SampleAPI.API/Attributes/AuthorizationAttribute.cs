using SampleAPI.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleAPI.API.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public AuthorizationAttribute() : base()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }
    }
}