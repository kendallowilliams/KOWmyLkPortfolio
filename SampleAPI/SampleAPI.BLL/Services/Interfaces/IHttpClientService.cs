using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.BLL.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> Get<T>(Uri requestUri, string userName, string password);

        Task<T> Post<T>(Uri requestUri, string userName, string password);
    }
}
