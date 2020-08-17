using Newtonsoft.Json;
using SampleAPI.DAL.Models.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SampleAPI.DAL.Models
{
    public partial class APIProfileService : IDataModel
    {
        public IEnumerable<ServiceDefinedField> GetServiceDefinedFields()
        {
            return !string.IsNullOrWhiteSpace(ServiceDefinedFields) ?
                JsonConvert.DeserializeObject<IEnumerable<ServiceDefinedField>>(ServiceDefinedFields) :
                Enumerable.Empty<ServiceDefinedField>();
        }
    }
}
