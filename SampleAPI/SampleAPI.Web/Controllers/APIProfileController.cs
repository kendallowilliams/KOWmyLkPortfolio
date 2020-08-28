using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;
using SampleAPI.DAL.Services.Interfaces;
using SampleAPI.DAL.Models;
using Newtonsoft.Json;
using SampleAPI.DAL.Extensions;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIProfile), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIProfileController : BaseController
    {
        private readonly APIProfileViewModel apiProfileViewModel;
        private readonly IDataService dataService;

        [ImportingConstructor]
        public APIProfileController(APIProfileViewModel apiProfileViewModel, IDataService dataService) 
        {
            this.apiProfileViewModel = apiProfileViewModel;
            this.dataService = dataService;
        }

        public async Task<ActionResult> Index(int? id)
        {
            apiProfileViewModel.APIProfiles = await dataService.GetList<APIProfile>();
            apiProfileViewModel.SelectedProfileId = id;

            return View(apiProfileViewModel);
        }

        public async Task<int> AddAPIProfile(APIProfile profile)
        {
            IEnumerable<APIProfile> profiles = await dataService.GetList<APIProfile>(item => item.Name.Equals(profile.Name) ||
                                                                                             item.UserName.Equals(profile.UserName) ||
                                                                                             item.Id == profile.Id);

            if (!profiles.Any())
            {
                profile.CreatedBy = profile.ModifiedBy = Request.UserHostAddress;
                await dataService.Insert(profile);
            }

            return profile.Id;
        }

        public async Task DeleteAPIProfile(int profileId)
        {
            APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == profileId);

            if (profile != null)
            {
                await dataService.Delete<APIProfile>(profile.Id);
            }
        }

        public async Task<ActionResult> APIProfile(int id)
        {
            APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == id, default, item => item.APIProfileService);
            IEnumerable<int> activeServiceIds = profile.APIProfileService.Select(item => item.APIServiceId);

            profile.HasAllServices = await dataService.Exists<APIService>(item => !activeServiceIds.Contains(item.Id))
                                                      .ContinueWith(task => !task.Result);

            return PartialView("~/Views/APIProfile/APIProfile.cshtml", profile);
        }

        public async Task<ActionResult> UpdateAPIProfile(APIProfile profile)
        {
            if (profile != null)
            {
                profile.ModifiedBy = Request.UserHostAddress;
                await dataService.Update(profile);
            }

            return new RedirectResult(Url.Action("Index", "APIProfile", new { id = profile?.Id }));
        }

        public async Task<ActionResult> ActiveServices(int profileId)
        {
            IEnumerable<APIProfileService> activeProfileServices = await dataService.GetList<APIProfileService>(item => item.APIProfileId == profileId,
                                                                                                                default,
                                                                                                                item => item.APIService);

            return PartialView("~/Views/APIProfile/ActiveServices.cshtml", activeProfileServices);
        }

        public async Task<ActionResult> InactiveServices(int profileId)
        {
            IEnumerable<int> activeIds = await dataService.GetList<APIProfileService>(item => item.APIProfileId == profileId)
                                                          .ContinueWith(task => task.Result.Select(item => item.APIServiceId));
            IEnumerable<APIService> inactiveServices = await dataService.GetList<APIService>(item => !activeIds.Contains(item.Id));

            return PartialView("~/Views/APIProfile/InactiveServices.cshtml", (profileId, inactiveServices));
        }

        public async Task AddProfileService(int profileId, int serviceId)
        {
            APIProfileService profileService = new APIProfileService()
            {
                APIProfileId = profileId,
                APIServiceId = serviceId,
                CreatedBy = Request.UserHostAddress,
                ModifiedBy = Request.UserHostAddress
            };

            await dataService.Insert(profileService);
        }

        public async Task RemoveProfileService(int profileServiceId)
        {
            await dataService.Delete<APIProfileService>(profileServiceId);
        }

        public async Task<ActionResult> ServiceDefinedFields(int profileServiceId)
        {
            APIProfileService link = await dataService.Get<APIProfileService>(item => item.Id == profileServiceId, default, item => item.APIService);
            IEnumerable<ServiceDefinedField> existingFields = link.GetServiceDefinedFields(),
                                             serviceFields = link.APIService.GetServiceDefinedFields();

            foreach(ServiceDefinedField field in serviceFields)
            {
                ServiceDefinedField existingField = existingFields.GetField(field.Name, field.Type);

                if (existingField != null) /*then*/ field.Value = existingField.Value;
            }

            return PartialView("~/Views/APIProfile/ServiceDefinedFields.cshtml", serviceFields);
        }

        public async Task UpdateAPIServiceDefinedFields(int profileServiceId, IEnumerable<ServiceDefinedField> fields)
        {
            APIProfileService service = await dataService.Get<APIProfileService>(item => item.Id == profileServiceId);

            if (fields == null) /*then*/ fields = Enumerable.Empty<ServiceDefinedField>();
            service.ServiceDefinedFields = JsonConvert.SerializeObject(fields);
            service.ModifiedBy = Request.UserHostAddress;
            await dataService.Update(service);
        }

        public async Task<ActionResult> APIAccessLogs(int profileId, int days)
        {
            DateTime pastDate = DateTime.Now.Date.AddDays(-days);
            IEnumerable<APIAccessLog> logs = await dataService.GetList<APIAccessLog>(item => item.APIProfileService.APIProfileId == profileId && item.CreatedOn >= pastDate);

            return PartialView("~/Views/APIProfile/APIAccessLogs.cshtml", (profileId, logs));
        }
    }
}