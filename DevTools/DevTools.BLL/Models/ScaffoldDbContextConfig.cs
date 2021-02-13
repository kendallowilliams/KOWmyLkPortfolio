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
    }
}
