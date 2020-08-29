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
using SampleAPI.DAL.Extensions;
using SampleAPI.API.Services.Interfaces;

namespace SampleAPI.API.Controllers
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SampleAPIController : BaseController
    {
        private readonly IDataService dataService;
        private readonly ISampleAPIService sampleAPIService;

        [ImportingConstructor]
        public SampleAPIController(IDataService dataService, ISampleAPIService sampleAPIService) : base()
        {
            this.dataService = dataService;
            this.sampleAPIService = sampleAPIService;
        }

        public async Task<IEnumerable<string>> AsyncTest()
        {
            Request.Headers.TryGetValues("APIProfileServiceId", out IEnumerable<string> profileServiceIds);
            int.TryParse(profileServiceIds?.FirstOrDefault(), out int linkId);
            APIProfileService link = await dataService.Get<APIProfileService>(item => item.Id == linkId);
            IEnumerable<ServiceDefinedField> fields = link.GetServiceDefinedFields();
            int numberOfTasks = fields.GetField("NumberOfTasks")?.GetIntValue() ?? 0;

            return await sampleAPIService.ExecuteDelayedTasks(numberOfTasks);
        }

        public int? Fibonacci(int position)
        {
            int? result = default;

            if (position >= 0)
            {
                result = sampleAPIService.Fibonacci(position);
            }

            return result;
        }

        public IEnumerable<int> FibonacciSequence(int position)
        {
            IEnumerable<int> results = Enumerable.Empty<int>();

            if (position >= 0)
            {
                results = sampleAPIService.FibonacciSequence(position);
            }

            return results;
        }
    }
}
