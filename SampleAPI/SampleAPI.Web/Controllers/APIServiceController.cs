﻿using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;
using SampleAPI.DAL.Services.Interfaces;
using System.Reflection;
using Newtonsoft.Json;
using SampleAPI.DAL.Models;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIService), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIServiceController : BaseController
    {
        private readonly APIServiceViewModel apiServiceViewModel;
        private readonly IDataService dataService;

        [ImportingConstructor]
        public APIServiceController(APIServiceViewModel apiServiceViewModel, IDataService dataService) : base()
        {
            this.apiServiceViewModel = apiServiceViewModel;
            this.dataService = dataService;
        }

        public async Task<ActionResult> Index(int? id)
        {
            apiServiceViewModel.APIServices = await dataService.GetList<APIService>();
            apiServiceViewModel.SelectedServiceId = id;

            return View(apiServiceViewModel);
        }

        public async Task<int> AddAPIService(APIService service)
        {
            APIService existingService = await dataService.Get<APIService>(item => item.Name.Equals(service.Name) ||
                                                                                   item.Id == service.Id);

            if (existingService == null)
            {
                service.CreatedBy = service.ModifiedBy = Request.UserHostAddress;
                await dataService.Insert(service);
            }

            return service.Id;
        }

        public async Task DeleteAPIService(int serviceId)
        {
            APIService service = await dataService.Get<APIService>(item => item.Id == serviceId);

            if (service != null)
            {
                await dataService.Delete<APIService>(service.Id);
            }
        }

        public async Task<ActionResult> APIService(int id)
        {
            APIService service = await dataService.GetAlt<APIService>(item => item.Id == id, default, "APIProfileService.APIProfile");

            return PartialView("~/Views/APIService/APIService.cshtml", service);
        }

        public async Task<ActionResult> UpdateAPIService(APIService service)
        {
            if (service != null)
            {
                service.ModifiedBy = Request.UserHostAddress;
                await dataService.Update(service);
            }

            return new RedirectResult(Url.Action("Index", "APIService", new { id = service?.Id }));
        }

        public async Task UpdateAPIServiceDefinedFields(int serviceId, IEnumerable<ServiceDefinedField> fields)
        {
            APIService service = await dataService.Get<APIService>(item => item.Id == serviceId);

            if (fields == null) /*then*/ fields = Enumerable.Empty<ServiceDefinedField>();
            service.ServiceDefinedFields = JsonConvert.SerializeObject(fields);
            service.ModifiedBy = Request.UserHostAddress;
            await dataService.Update(service);
        }
    }
}