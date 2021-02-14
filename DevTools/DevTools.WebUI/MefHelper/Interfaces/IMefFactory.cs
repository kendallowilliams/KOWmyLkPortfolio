using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.WebUI.MefHelper.Interfaces
{
    public interface IMefFactory
    {
        T GetExportedValue<T>();
    }
}
