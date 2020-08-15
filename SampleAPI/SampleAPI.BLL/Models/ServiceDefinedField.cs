using SampleAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.BLL.Models
{
    public class ServiceDefinedField : IServiceDefinedField
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public string Value { get; set; }
    }
}
