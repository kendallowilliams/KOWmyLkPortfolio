using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Shared.Models
{
    public class DevToolsSettings
    {
        public DevToolsSettings()
        {
            DotNetExe = "dotnet";
            DotNetEfExe = "dotnet-ef";
        }

        public string DotNetExe { get; set; }
        
        public string DotNetEfExe { get; set; }
    }
}
