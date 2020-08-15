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

        public int? GetIntValue() => Type == APIServiceDataTypes.INT && int.TryParse(Value, out int result) ? (int?)result : null;

        public bool? GetBooleanValue() => Type == APIServiceDataTypes.BOOLEAN && bool.TryParse(Value, out bool result) ? (bool?)result : null;

        public DateTime? GetDateTimeValue() => Type == APIServiceDataTypes.DATETIME && DateTime.TryParse(Value, out DateTime result) ? (DateTime?)result : null;

        public string GetStringValue() => Type == APIServiceDataTypes.STRING ? Value : null;
    }
}
