using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.BLL.Services.Interfaces
{
    public interface IHttpClientService: IDisposable
    {
        Task<T> Get<T>(Uri requestUri);

        Task<T> Post<T>(Uri requestUri);
    }
}
