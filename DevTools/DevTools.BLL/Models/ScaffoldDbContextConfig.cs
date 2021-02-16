using DevTools.BLL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DevTools.BLL.Models
{
    public class ScaffoldDbContextConfig : IScaffoldDbContextConfig
    {
        public ScaffoldDbContextConfig()
        {
            Tables = Enumerable.Empty<string>();
            Provider = "Microsoft.EntityFrameworkCore.SqlServer";
        }

        [Required]
        public string ConnectionString { get; set; }
        [Required]
        public string Provider { get; set; }
        public bool DataAnnotations { get; set; }
        [Required]
        public string Context { get; set; }
        [Required]
        public string ContextDir { get; set; }
        public string ContextNameSpace { get; set; } /*EF Core 5.0*/
        public bool Force { get; set; }
        [Required]
        public string OutputDir { get; set; }
        public string NameSpace { get; set; } /*EF Core 5.0*/
        public string Schema { get; set; }
        public IEnumerable<string> Tables { get; set; }
        public bool UseDatabaseNames { get; set; }
        public bool NoOnConfiguring { get; set; } /*EF Core 5.0*/
        public bool NoPluralize { get; set; }
        public string Project { get; set; }
        public string StartupProject { get; set; }
        public bool Verbose { get; set; }

        public string BuildArgumentList(string configuration)
        {
            List<string> argumentLists = new List<string>();
            string tableNames = string.Join(" ", (Tables ?? Enumerable.Empty<string>()).Select(table => $"-t {table}")),
                   arguments = string.Empty;

            argumentLists.Add($"\"{ConnectionString}\"");
            argumentLists.Add(Provider);
            argumentLists.Add($"--project \"{Project}\"");
            argumentLists.Add($"--startup-project \"{StartupProject}\"");
            if (DataAnnotations) /*then*/ argumentLists.Add("--data-annotations");
            argumentLists.Add($"--context {Context}");
            argumentLists.Add($"--context-dir \"{ContextDir}\"");
            if (!string.IsNullOrWhiteSpace(ContextNameSpace)) /*then*/argumentLists.Add($"--context-namespace {ContextNameSpace}");
            if (Force) /*then*/ argumentLists.Add("--force");
            argumentLists.Add($"--output-dir \"{OutputDir}\"");
            if (!string.IsNullOrWhiteSpace(NameSpace)) /*then*/argumentLists.Add($"--namespace {NameSpace}");
            if (!string.IsNullOrWhiteSpace(Schema)) /*then*/ argumentLists.Add($"--schema {Schema}");
            argumentLists.Add(tableNames);
            if (UseDatabaseNames) /*then*/ argumentLists.Add("--use-database-names");
            if (NoOnConfiguring) /*then*/ argumentLists.Add("--no-onconfiguring");
            if (NoPluralize) /*then*/ argumentLists.Add("--no-pluralize");
            if (Verbose) /*then*/ argumentLists.Add("--verbose");
            argumentLists.Add($"--configuration {configuration}");
            arguments = string.Join(" ", argumentLists);

            return arguments;
        }
    }
}
