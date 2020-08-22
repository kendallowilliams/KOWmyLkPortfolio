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
        [ImportingConstructor]
        public SampleAPIController() : base()
        {

        }

        public async Task<IEnumerable<string>> AsyncTest([FromBody] int numberOfTasks)
        {
            IEnumerable<string> results = Enumerable.Empty<string>();
            Random rand = new Random(DateTime.Now.Millisecond);

            var tasks = Enumerable.Range(0, numberOfTasks)
                                  .Select(index => new { Index = index, Start = DateTime.Now, Delay = rand.Next() % 5 })
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
