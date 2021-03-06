﻿using SampleAPI.API.Services.Interfaces;
using SampleAPI.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.API.Services
{
    [Export(typeof(ISampleAPIService)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SampleAPIService : ISampleAPIService
    {
        private readonly ITPLService tplService;

        [ImportingConstructor]
        public SampleAPIService(ITPLService tplService)
        {
            this.tplService = tplService;
        }

        public async Task<IEnumerable<string>> ExecuteDelayedTasks(int numberOfTasks)
        {
            IEnumerable<string> results = Enumerable.Empty<string>();
            Random rand = new Random(DateTime.Now.Millisecond);
            int maxDelay = 5;

            var data = Enumerable.Range(0, numberOfTasks)
                                 .Select(index => new { Index = index, Start = DateTime.Now, Delay = (rand.Next() % maxDelay) + 1 /* 1 to maxDelay */ });

            results = await tplService.ConcurrentAsync(async item => await Task.Delay(item.Delay * 1000)
                                                                               .ContinueWith(_ => $"Task: {item.Index}, Delay: {item.Delay}, Start: {item.Start}, End: {DateTime.Now}"),
                                                                               data,
                                                                               numberOfTasks);

            return results;
        }

        public ulong Fibonacci(int sequenceIndex)
        {
            int startPosition = 2; // exclude 0 and 1
            ulong[] sequence = sequenceIndex >= startPosition ? new ulong[sequenceIndex + 1] : new ulong[startPosition]; // default to 0 and 1 only

            sequence[0] = 0;
            sequence[1] = 1;

            if (sequenceIndex >= startPosition)
            {
                for (int index = startPosition; index < sequence.Length; index++)
                {
                    sequence[index] = sequence[index - 1] + sequence[index - 2];
                }
            }

            return sequenceIndex >= 0 ? sequence[sequenceIndex] : 0;
        }

        public IEnumerable<ulong> FibonacciSequence(int sequenceQuantity)
        {
            int startPosition = 2; // exclude 0 and 1
            ulong[] sequence = sequenceQuantity > startPosition ? new ulong[sequenceQuantity] : new ulong[startPosition]; // default to 0 and 1 only

            sequence[0] = 0;
            sequence[1] = 1;

            if (sequenceQuantity > startPosition)
            {
                for (int index = startPosition; index < sequenceQuantity; index++)
                {
                    sequence[index] = sequence[index - 1] + sequence[index - 2];
                }
            }

            return sequence.Take(sequenceQuantity).OrderBy(item => item);
        }
    }
}
