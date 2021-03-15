using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Shared.Models.Interfaces
{
    public interface IScaffoldDbContextConfig
    {
        string ConnectionString { get; set; }
        string Provider { get; set; }
        bool DataAnnotations { get; set; }
        string Context { get; set; }
        string ContextDir { get; set; }
        string ContextNameSpace { get; set; } /*EF Core 5.0*/
        bool Force { get; set; }
        string OutputDir { get; set; }
        string NameSpace { get; set; } /*EF Core 5.0*/
        string Schema { get; set; }
        IEnumerable<string> Tables { get; set; }
        bool UseDatabaseNames { get; set; }
        bool NoOnConfiguring { get; set; } /*EF Core 5.0*/
        bool NoPluralize { get; set; }
        string Project { get; set; }
        string StartupProject { get; set; }
        bool Verbose { get; set; }

        string BuildArgumentList(string configuration);
    }
}
