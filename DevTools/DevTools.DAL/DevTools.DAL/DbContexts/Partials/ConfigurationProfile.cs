using DevTools.DAL.DbContexts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevTools.DAL.Enums;

namespace DevTools.DAL.DbContexts
{
    public partial class ConfigurationProfile : IConfigurationProfile
    {
        public ConfigurationProfile()
        {
        }

        public virtual string Type { get; set; }
        public virtual string Json { get; set; }

        public T GetProfile<T>()
        {
            return GetConfigurationProfileType() == ConfigurationProfileType.ScaffoldDbContext && !string.IsNullOrWhiteSpace(Json) ? 
                JsonConvert.DeserializeObject<T>(Json) : 
                default(T);
        }

        public ConfigurationProfileType GetConfigurationProfileType()
        {
            return Enum.TryParse(Type, out ConfigurationProfileType type) ? type : ConfigurationProfileType.Unknown;
        }
    }
}
