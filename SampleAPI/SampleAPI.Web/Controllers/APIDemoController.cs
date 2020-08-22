using SampleAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Controllers
{
    [Export(nameof(Pages.APIDemo), typeof(IController)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class APIDemoController : BaseController
    {
        private readonly APIDemoViewModel apiDemoViewModel;

        [ImportingConstructor]
        public APIDemoController(APIDemoViewModel apiDemoViewModel) : base()
        {
            this.apiDemoViewModel = apiDemoViewModel;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View(apiDemoViewModel));
        }

        public async Task<JsonResult> AsyncTest(int numberOfTasks)
        {
            HttpResponseMessage message = default;
            JsonResult result = default;
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("numberOfTasks", numberOfTasks.ToString())
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            using (var client = new HttpClient())
            {
                client.BaseAddress = apiUri;
                message = await client.PostAsync("SampleAPI/AsyncTest", content);

                if (message.IsSuccessStatusCode)
                {
                    result = Json(await message.Content.ReadAsStringAsync());
                }
            }

            return result;
        }
    }
}