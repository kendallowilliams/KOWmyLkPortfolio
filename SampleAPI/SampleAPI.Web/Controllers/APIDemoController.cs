using Newtonsoft.Json;
using SampleAPI.BLL.Services.Interfaces;
using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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

        [ImportingConstructor]
        public APIDemoController(APIDemoViewModel apiDemoViewModel, IHttpClientService httpClientService) : base()
        {
            this.apiDemoViewModel = apiDemoViewModel;
            this.httpClientService = httpClientService;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View(apiDemoViewModel));
        }

        public async Task<string> AsyncTest(int numberOfTasks)
        {
            Uri path = new Uri(apiUri, $"SampleAPI/AsyncTest?numberOfTasks={numberOfTasks}");
            IEnumerable<string> results = await httpClientService.Post<IEnumerable<string>>(path);

            return string.Join(Environment.NewLine, results);
        }
    }
}