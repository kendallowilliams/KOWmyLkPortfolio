using Microsoft.EntityFrameworkCore;
using SampleAPI.BLL.Models;
using SampleAPI.BLL.Services.Interfaces;
using SampleAPI.DAL.DbContexts;
using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Windows.Forms;

namespace SampleAPI.API.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public AuthorizationAttribute() : base()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            APIProfileService link = default;
            HttpRequestMessage request = actionContext.Request;
            string profileId = request.Headers.TryGetValues(nameof(APIProfileService.APIProfileId), out IEnumerable<string> ids) ? ids.FirstOrDefault() : string.Empty,
                   controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName,
                   action = actionContext.ActionDescriptor.ActionName;
            int.TryParse(profileId, out int safeProfileId);

            using (var db = new SampleAPIContext())
            {
                IQueryable<APIProfileService> query = db.APIProfileService.Include(item => item.APIService);

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                link = query.FirstOrDefault(item => item.APIService.Controller.Equals(controller) &&
                                                    item.APIService.Action.Equals(action) &&
                                                    item.APIProfileId == safeProfileId);

                if (link == null)
                {
                    actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "API profile not authorized to use this service");
                }
                else if (link.APIService.Disabled)
                {
                    APIService service = link.APIService;
                    string savedCode = service.DisabledResponseCode.HasValue ? service.DisabledResponseCode.ToString() : string.Empty,
                           statusMessage = !string.IsNullOrWhiteSpace(service.DisabledResponseMessage) ? service.DisabledResponseMessage : "API Service Unavailable";

                    if (!Enum.TryParse(savedCode, out HttpStatusCode code))
                    {
                        code = HttpStatusCode.ServiceUnavailable;
                    }
                    
                    actionContext.Response = request.CreateErrorResponse(code, statusMessage);
                }
                else
                {
                    request.Headers.Add(nameof(APIAccessLog.APIProfileServiceId), link.Id.ToString());
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}