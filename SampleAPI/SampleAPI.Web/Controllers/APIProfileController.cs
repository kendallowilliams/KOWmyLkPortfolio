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
using SampleAPI.BLL.Models;
using Newtonsoft.Json;

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

        public async Task<ActionResult> APIProfile(int id)
        {
            APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == id);

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
            IEnumerable<ServiceDefinedField> existingFields = !string.IsNullOrWhiteSpace(link.ServiceDefinedFields) ?
                JsonConvert.DeserializeObject<IEnumerable<ServiceDefinedField>>(link.ServiceDefinedFields) :
                Enumerable.Empty<ServiceDefinedField>(),
                                             serviceFields = !string.IsNullOrWhiteSpace(link.APIService.ServiceDefinedFields) ?
                JsonConvert.DeserializeObject<IEnumerable<ServiceDefinedField>>(link.APIService.ServiceDefinedFields) :
                Enumerable.Empty<ServiceDefinedField>();

            foreach(ServiceDefinedField field in serviceFields)
            {
                ServiceDefinedField existingField = existingFields.FirstOrDefault(item => item.Name.Equals(field.Name, StringComparison.OrdinalIgnoreCase) &&
                                                                                          item.Type == field.Type);

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
    }
}