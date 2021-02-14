using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.BLL.Models.Interfaces
{
    public interface IScaffoldDbContextProfile
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string ProjectLocation { get; set; }

        string StartupProjectLocation { get; set; }

        ScaffoldDbContextConfig ScaffoldDbContextConfig { get; set; }
    }
}
