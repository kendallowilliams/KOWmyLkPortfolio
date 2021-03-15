using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Shared.Models.Interfaces
{
    public interface IScaffoldDbContextProfile
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string ProjectLocation { get; set; }

        string StartupProjectLocation { get; set; }

        string BuildConfiguration { get; set; }

        bool Verbose { get; set; }

        ScaffoldDbContextConfig ScaffoldDbContextConfig { get; set; }

        bool IsValid(out IEnumerable<string> errors);

        string GetProjectBuildArguments(string outputPath);

        string GetStartupProjectBuildArguments(string outputPath);
        
        string GetProjectCleanArguments();

        string GetStartupProjectCleanArguments();
    }
}
