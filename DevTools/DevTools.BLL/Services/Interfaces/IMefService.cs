using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.DLL.Services.Interfaces
{
    public interface IMefService : IDisposable
    {
        Lazy<T> GetExport<T>();

        T GetExportedValue<T>();
    }
}
