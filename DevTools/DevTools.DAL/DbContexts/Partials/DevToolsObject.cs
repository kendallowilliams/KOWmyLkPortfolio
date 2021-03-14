using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DevTools.Shared.Enums;

namespace DevTools.DAL.Models
{
    public partial class DevToolsObject
    {
        public DevToolsObject()
        {
        }

        public T GetObject<T>()
        {
            return !string.IsNullOrWhiteSpace(ObjectJson) ? JsonConvert.DeserializeObject<T>(ObjectJson) : default(T);
        }

        public DevToolsObjectType GetDevToolsObjectType()
        {
            return !string.IsNullOrWhiteSpace(ObjectType) && Enum.TryParse(ObjectType, out DevToolsObjectType objectType) ? objectType : DevToolsObjectType.Unknown;
        }
    }
}
