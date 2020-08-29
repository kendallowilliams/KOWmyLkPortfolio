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
        int Fibonacci(int position, List<int> storage = null);
        IEnumerable<int> FibonacciSequence(int position);
    }
}
