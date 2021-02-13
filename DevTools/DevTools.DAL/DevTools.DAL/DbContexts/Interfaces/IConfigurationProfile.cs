using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.DAL.DbContexts.Interfaces
{
    public interface IConfigurationProfile
    {
        string Type { get; set; }

        string Json { get; set; }
    }
}
