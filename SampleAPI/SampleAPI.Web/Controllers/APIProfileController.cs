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
    }
}