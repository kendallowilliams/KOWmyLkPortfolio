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
    public partial class ConfigurationProfile
    {
        public ConfigurationProfile()
        {
        }

        public T GetProfile<T>()
        {
            PropertyInfo typeProperty = typeof(ConfigurationProfile).GetProperty(nameof(Newtonsoft.Json));
            string json = typeProperty.GetValue(this) as string;

            return GetConfigurationProfileType() == ConfigurationProfileType.ScaffoldDbContext && !string.IsNullOrWhiteSpace(json) ? 
                JsonConvert.DeserializeObject<T>(json) : 
                default(T);
        }

        public ConfigurationProfileType GetConfigurationProfileType()
        {
            PropertyInfo typeProperty = typeof(ConfigurationProfile).GetProperty(nameof(Type));
            string type = typeProperty.GetValue(this) as string;

            return Enum.TryParse(type, out ConfigurationProfileType configType) ? configType : ConfigurationProfileType.Unknown;
        }
    }
}
