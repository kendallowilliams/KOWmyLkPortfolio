using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Shared
{
    public static class Enums
    {
        public enum Status { Created, Processing, Completed, Errored, Cancelled, Unknown = 0 }

        public enum DevToolsObjectType { ScaffoldDbContextProfile, DevToolsSettings, Unknown = 0 }

        public enum ProcessorItemType { ScaffoldDbContextProfile, Unknown = 0 }
    }
}
