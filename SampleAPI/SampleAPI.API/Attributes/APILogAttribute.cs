using Microsoft.EntityFrameworkCore;
using SampleAPI.DAL.DbContexts;
using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Windows.Forms;

namespace SampleAPI.API.Attributes
{
    public class APILogAttribute : ActionFilterAttribute
    {
        public APILogAttribute() : base() { }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            string linkId = request.Headers.TryGetValues(nameof(APIAccessLog.APIProfileServiceId), out IEnumerable<string> ids) ? ids.FirstOrDefault() : string.Empty;
            int.TryParse(linkId, out int safeLinkId);
            APIAccessLog log = new APIAccessLog()
            {
                APIProfileServiceId = safeLinkId,
                IPAddress = HttpContext.Current.Request.UserHostAddress,
                CreatedBy = HttpContext.Current.Request.UserHostAddress,
                ModifiedBy = HttpContext.Current.Request.UserHostAddress
            };

            using (var db = new SampleAPIContext())
            {
                APIProfileService link = db.APIProfileService.FirstOrDefault(item => item.Id == safeLinkId);

                if (link != null)
                {
                    db.APIAccessLog.Add(log);
                    db.SaveChanges();
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}