using DevTools.BLL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.BLL.Services.Interfaces
{
    public interface IConsoleService
    {
        string Process(IScaffoldDbContextProfile profile);
        string Build(IScaffoldDbContextProfile profile, string outputPath);
        string ScaffoldDbContext(IScaffoldDbContextConfig config);
    }
}
