using Newtonsoft.Json;
using SampleAPI.BLL.Services.Interfaces;
using SampleAPI.DAL.Models;
using SampleAPI.DAL.Services.Interfaces;
using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIDemo), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIDemoController : BaseController
    {
        private readonly APIDemoViewModel apiDemoViewModel;
        private readonly IHttpClientService httpClientService;
        private readonly IDataService dataService;

        [ImportingConstructor]
        public APIDemoController(APIDemoViewModel apiDemoViewModel, IHttpClientService httpClientService,
                                 IDataService dataService) : base()
        {
            this.apiDemoViewModel = apiDemoViewModel;
            this.httpClientService = httpClientService;
            this.dataService = dataService;
        }

        public async Task<ActionResult> Index()
        {
            apiDemoViewModel.APIProfileServices = await dataService.GetList<APIProfileService>(default, 
                default, 
                item => item.APIProfile,
                item => item.APIService);
            apiDemoViewModel.APIServices = await dataService.GetList<APIService>();

            return View(apiDemoViewModel);
        }

        public async Task<string> AsyncTest(int profileId)
        {
            string result = string.Empty;

            try
            {
                APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == profileId);
                Uri path = new Uri(apiUri, Url.Action("AsyncTest", "SampleAPI"));
                IEnumerable<string> results = await httpClientService.Post<IEnumerable<string>>(path, profile.UserName, profile.Password) ??
                                              Enumerable.Empty<string>();

                result = string.Join(Environment.NewLine, results);
            }
            catch(HttpRequestException ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> Fibonacci(int profileId)
        {
            string result = string.Empty;

            try
            {
                APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == profileId);
                Uri path = new Uri(apiUri, Url.Action("Fibonacci", "SampleAPI"));
                result = await httpClientService.Post<string>(path, profile.UserName, profile.Password);
            }
            catch (HttpRequestException ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> FibonacciSequence(int profileId)
        {
            string result = string.Empty;

            try
            {
                APIProfile profile = await dataService.Get<APIProfile>(item => item.Id == profileId);
                Uri path = new Uri(apiUri, Url.Action("FibonacciSequence", "SampleAPI"));
                IEnumerable<ulong> sequence = await httpClientService.Post<IEnumerable<ulong>>(path, profile.UserName, profile.Password);

                result = string.Join(", ", sequence);
            }
            catch (HttpRequestException ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}