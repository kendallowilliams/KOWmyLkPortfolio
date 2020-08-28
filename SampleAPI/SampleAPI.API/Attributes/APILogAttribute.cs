using Microsoft.EntityFrameworkCore;
using SampleAPI.DAL.DbContexts;
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
            using (var db = new SampleAPIContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}