using SampleAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static SampleAPI.DAL.Enums;

namespace SampleAPI.BLL.Models
{
    public class ServiceDefinedField : IServiceDefinedField
    {
        public string Name { get; set; }

        public APIServiceDataTypes Type { get; set; }

        public string Value { get; set; }
    }
}
