using Newtonsoft.Json;
using SampleAPI.DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.DAL.Models
{
    public partial class APIService : IDataModel
    {
        public IEnumerable<ServiceDefinedField> GetServiceDefinedFields()
        {
            return !string.IsNullOrWhiteSpace(ServiceDefinedFields) ? 
                JsonConvert.DeserializeObject<IEnumerable<ServiceDefinedField>>(ServiceDefinedFields) : 
                Enumerable.Empty<ServiceDefinedField>();
        }
    }
}
