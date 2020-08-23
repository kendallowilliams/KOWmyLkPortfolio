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
                link = db.APIProfileService.FirstOrDefault(item => item.APIService.Controller.Equals(controller, StringComparison.OrdinalIgnoreCase) &&
                                                                    item.APIService.Action.Equals(action, StringComparison.OrdinalIgnoreCase) &&
                                                                    item.APIProfileId == safeProfileId);

                if (link == null)
                {
                    actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "API profile not authorized to use this service");
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