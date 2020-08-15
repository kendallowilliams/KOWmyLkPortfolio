using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Models
{
    [Export]
    public class APIServiceViewModel : BaseViewModel
    {
        [ImportingConstructor]
        public APIServiceViewModel() : base(Pages.APIService)
        {
            APIServices = Enumerable.Empty<APIService>();
        }

        public IEnumerable<APIService> APIServices { get; set; }
        public int? SelectedServiceId { get; set; }
        public static IEnumerable<SelectListItem> HttpResponseCodes
        {
            get => Enum.GetValues(typeof(HttpStatusCode)).Cast<int>()
                       .Select(value => new SelectListItem
                       {
                           Text = $"{value}: {Enum.GetName(typeof(HttpStatusCode), value)}",
                           Value = value.ToString()
                       });
        }
    }
}