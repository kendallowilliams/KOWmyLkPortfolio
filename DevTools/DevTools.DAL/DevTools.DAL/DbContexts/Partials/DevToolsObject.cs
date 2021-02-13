using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DevTools.DAL.Enums;

namespace DevTools.DAL.DbContexts
{
    public partial class DevToolsObject
    {
        public DevToolsObject()
        {
        }

        public T GetObject<T>()
        {
            PropertyInfo typeProperty = typeof(DevToolsObject).GetProperty(nameof(Newtonsoft.Json));
            string json = typeProperty.GetValue(this) as string;

            return !string.IsNullOrWhiteSpace(json) ? JsonConvert.DeserializeObject<T>(json) : default(T);
        }

        public DevToolsObjectType GetDevToolsObjectType()
        {
            PropertyInfo typeProperty = typeof(DevToolsObject).GetProperty(nameof(Type));
            string type = typeProperty.GetValue(this) as string;

            return !string.IsNullOrWhiteSpace(type) && Enum.TryParse(type, out DevToolsObjectType configType) ? configType : DevToolsObjectType.Unknown;
        }
    }
}
