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

        public ulong Fibonacci(int sequenceIndex)
        {
            ulong[] sequence = new ulong[sequenceIndex + 1];
            int startPosition = 2; // exclude 0 and 1

            sequence[0] = 0;
            if (sequenceIndex > 0) /*then*/ sequence[1] = 1;

            if (sequenceIndex > 1)
            {
                for (int index = startPosition; index < sequence.Length; index++)
                {
                    sequence[index] = sequence[index - 1] + sequence[index - 2];
                }
            }

            return sequence[sequenceIndex];
        }

        public IEnumerable<ulong> FibonacciSequence(int sequenceQuantity)
        {
            ulong[] sequence = new ulong[sequenceQuantity];
            int startPosition = 2; // exclude 0 and 1

            sequence[0] = 0;
            sequence[1] = 1;

            if (sequenceQuantity > 1)
            {
                for (int index = startPosition; index < sequenceQuantity; index++)
                {
                    sequence[index] = sequence[index - 1] + sequence[index - 2];
                }
            }

            return sequence.Take(sequenceQuantity);
        }
    }
}
