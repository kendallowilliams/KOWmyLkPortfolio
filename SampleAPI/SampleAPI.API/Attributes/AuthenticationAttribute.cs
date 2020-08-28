using Microsoft.EntityFrameworkCore;
using SampleAPI.BLL.Models;
using SampleAPI.DAL.DbContexts;
using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleAPI.API.Attributes
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public AuthenticationAttribute() : base()
        {

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            APIProfile profile = default;
            HttpRequestMessage request = actionContext.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            HttpAuthentication authentication = new HttpAuthentication(authorization?.Scheme, authorization?.Parameter);

            if (authentication.IsValid)
            {
                using (var db = new SampleAPIContext())
                {
                    db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    profile = db.APIProfile.FirstOrDefault(item => item.UserName.Equals(authentication.UserName) &&
                                                                   item.Password.Equals(authentication.Password));

                    if (profile == null)
                    {
                        actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid username and/or password");
                    }
                    else
                    {
                        request.Headers.Add(nameof(APIProfileService.APIProfileId), profile.Id.ToString());
                    }
                }
            }
            else
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Basic authentication required");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}