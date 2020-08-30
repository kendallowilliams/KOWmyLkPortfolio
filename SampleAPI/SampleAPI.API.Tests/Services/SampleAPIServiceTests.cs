using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleAPI.API.Services;
using SampleAPI.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SampleAPI.API.Tests
{
    [TestClass]
    public class SampleAPIServiceTests
    {
        public SampleAPIServiceTests() { }

        [TestMethod]
        public void Fibonacci_SequenceIndexEqualsZero_ReturnsZero()
        {
            ISampleAPIService sampleAPIService = new SampleAPIService();
            ulong result = sampleAPIService.Fibonacci(0);

            Assert.AreEqual<ulong>(0, result);
        }

        [TestMethod]
        public void Fibonacci_SequenceIndexEqualsOne_ReturnsOne()
        {
            ISampleAPIService sampleAPIService = new SampleAPIService();
            ulong result = sampleAPIService.Fibonacci(1);

            Assert.AreEqual<ulong>(1, result);
        }

        [TestMethod]
        public void Fibonacci_SequenceIndexEqualsTwo_ReturnsOne()
        {
            ISampleAPIService sampleAPIService = new SampleAPIService();
            ulong result = sampleAPIService.Fibonacci(2);

            Assert.AreEqual<ulong>(1, result);
        }

        [TestMethod]
        public void Fibonacci_SequenceIndexEqualsTen_Returns55()
        {
            ISampleAPIService sampleAPIService = new SampleAPIService();
            ulong result = sampleAPIService.Fibonacci(10);

            Assert.AreEqual<ulong>(55, result);
        }

        [TestMethod]
        public void Fibonacci_SequenceIndexIsNegative_ReturnsZero()
        {
            ISampleAPIService sampleAPIService = new SampleAPIService();
            ulong result = sampleAPIService.Fibonacci(-1);

            Assert.AreEqual<ulong>(0, result);
        }

        [TestMethod]
        public void FibonacciSequence_SequenceQuantityEqualsZero_ReturnsZero()
        {
            int sequenceQuantity = 0;
            ISampleAPIService sampleAPIService = new SampleAPIService();
            IEnumerable<ulong> results = sampleAPIService.FibonacciSequence(sequenceQuantity);

            Assert.IsTrue(!results.Any());
        }

        [TestMethod]
        public void FibonacciSequence_SequenceQuantityIsOne_ReturnsSequence()
        {
            int sequenceQuantity = 1;
            ISampleAPIService sampleAPIService = new SampleAPIService();
            IEnumerable<ulong> results = sampleAPIService.FibonacciSequence(sequenceQuantity);
            IEnumerable<ulong> expected = Enumerable.Range(0, sequenceQuantity)
                                                    .Select(index => sampleAPIService.Fibonacci(index));

            Assert.AreEqual(string.Join(",", expected), string.Join(",", results));
        }

        [TestMethod]
        public void FibonacciSequence_SequenceQuantityIsTwo_ReturnsSequence()
        {
            int sequenceQuantity = 2;
            ISampleAPIService sampleAPIService = new SampleAPIService();
            IEnumerable<ulong> results = sampleAPIService.FibonacciSequence(sequenceQuantity);
            IEnumerable<ulong> expected = Enumerable.Range(0, sequenceQuantity)
                                                    .Select(index => sampleAPIService.Fibonacci(index));

            Assert.AreEqual(string.Join(",", expected), string.Join(",", results));
        }

        [TestMethod]
        public void FibonacciSequence_SequenceQuantityIsTen_ReturnsSequence()
        {
            int sequenceQuantity = 10;
            ISampleAPIService sampleAPIService = new SampleAPIService();
            IEnumerable<ulong> results = sampleAPIService.FibonacciSequence(sequenceQuantity);
            IEnumerable<ulong> expected = Enumerable.Range(0, sequenceQuantity)
                                                    .Select(index => sampleAPIService.Fibonacci(index));

            Assert.AreEqual(string.Join(",", expected), string.Join(",", results));
        }

        [TestMethod]
        public void FibonacciSequence_SequenceQuantityIsNegative_ReturnsEmptyList()
        {
            int sequenceQuantity = -1;
            ISampleAPIService sampleAPIService = new SampleAPIService();
            IEnumerable<ulong> results = sampleAPIService.FibonacciSequence(sequenceQuantity);

            Assert.IsTrue(!results.Any());
        }
    }
}
