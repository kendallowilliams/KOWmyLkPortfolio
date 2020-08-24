using SampleAPI.DAL.Models;
using SampleAPI.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;

namespace SampleAPI.API.Controllers
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SampleAPIController : BaseController
    {
        private readonly IDataService dataService;

        [ImportingConstructor]
        public SampleAPIController(IDataService dataService) : base()
        {
            this.dataService = dataService;
        }

        public async Task<IEnumerable<string>> AsyncTest()
        {
            IEnumerable<string> results = Enumerable.Empty<string>();
            Random rand = new Random(DateTime.Now.Millisecond);
            Request.Headers.TryGetValues("APIProfileServiceId", out IEnumerable<string> profileServiceIds);
            int.TryParse(profileServiceIds?.FirstOrDefault(), out int linkId);
            APIProfileService link = await dataService.Get<APIProfileService>(item => item.Id == linkId);
            IEnumerable<ServiceDefinedField> fields = link.GetServiceDefinedFields();
            int numberOfTasks = fields.FirstOrDefault(item => item.Name.Equals("NumberOfTasks", StringComparison.OrdinalIgnoreCase))?.GetIntValue() ?? 0,
                maxDelay = 6; // up to five seconds

            var tasks = Enumerable.Range(0, numberOfTasks)
                                  .Select(index => new { Index = index, Start = DateTime.Now, Delay = rand.Next() % maxDelay })
                                  .Select(item => Task.Delay(item.Delay * 1000).ContinueWith(_ => new
                                  {
                                    item.Index,
                                    item.Start,
                                    item.Delay,
                                    End = DateTime.Now
                                  }));
            results = await Task.WhenAll(tasks)
                                .ContinueWith(task => task.Result.Select(item => $"Task: {item.Index}, Delay: {item.Delay}, Start: {item.Start}, End: {item.End}"));

            return results;
        }
    }
}
