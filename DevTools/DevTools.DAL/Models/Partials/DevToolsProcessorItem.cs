using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DAL.Models
{
    public partial class DevToolsProcessorItem
    {
        public DevToolsProcessorItem()
        {
        }

        public T GetRequest<T>()
        {
            return string.IsNullOrWhiteSpace(RequestJson) ? default(T) : JsonConvert.DeserializeObject<T>(RequestJson);
        }
    }
}
