using SampleAPI.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.API.Services
{
    [Export(typeof(ISampleAPIService))]
    public class SampleAPIService : ISampleAPIService
    {
        [ImportingConstructor]
        public SampleAPIService()
        {

        }

        public async Task<IEnumerable<string>> ExecuteDelayedTasks(int numberOfTasks)
        {
            IEnumerable<string> results = Enumerable.Empty<string>();
            Random rand = new Random(DateTime.Now.Millisecond);
            int maxDelay = 5;

            var tasks = Enumerable.Range(0, numberOfTasks)
                                  .Select(index => new { Index = index, Start = DateTime.Now, Delay = (rand.Next() % maxDelay) + 1 /* 1 to maxDelay */ })
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

        public int Fibonacci(int position, List<int> storage = null)
        {
            int result = (position == 0 || position == 1) ? position : Fibonacci(position - 1) + Fibonacci(position - 2);

            if (storage != null) /*then*/ storage.Add(result);

            return result;
        }

        public IEnumerable<int> FibonacciSequence(int position)
        {
            List<int> storage = new List<int>();

            Fibonacci(position, storage);

            return storage;
        }
    }
}
