using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SampleAPI.DAL.Enums;

namespace SampleAPI.DAL.Extensions
{
    public static class ServiceDefinedFieldExtensions
    {
        public static ServiceDefinedField GetField(this IEnumerable<ServiceDefinedField> fields, string name)
        {
            if (fields == null) /*then*/ fields = Enumerable.Empty<ServiceDefinedField>();

            return fields.FirstOrDefault(field => field.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static ServiceDefinedField GetField(this IEnumerable<ServiceDefinedField> fields, string name, APIServiceDataTypes type)
        {
            if (fields == null) /*then*/ fields = Enumerable.Empty<ServiceDefinedField>();

            return fields.FirstOrDefault(field => field.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && field.Type == type);
        }
    }
}
