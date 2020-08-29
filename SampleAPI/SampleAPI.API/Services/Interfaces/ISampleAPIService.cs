using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.API.Services.Interfaces
{
    public interface ISampleAPIService
    {
        Task<IEnumerable<string>> ExecuteDelayedTasks(int numberOfTasks);
        ulong Fibonacci(int position);
        IEnumerable<ulong> FibonacciSequence(int position);
    }
}
