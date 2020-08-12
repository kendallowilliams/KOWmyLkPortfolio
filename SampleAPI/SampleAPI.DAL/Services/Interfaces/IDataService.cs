using SampleAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleAPI.DAL.Services.Interfaces
{
    public interface IDataService
    {
        Task<T> Get<T>(Expression<Func<T, bool>> expression = null, CancellationToken token = default(CancellationToken), params Expression<Func<T, object>>[] includes) where T : class, IDataModel;
        Task<IEnumerable<T>> GetList<T>(Expression<Func<T, bool>> expression = null, CancellationToken token = default(CancellationToken), params Expression<Func<T, object>>[] includes) where T : class, IDataModel;
        Task<int> Update<T>(T entity, CancellationToken token = default(CancellationToken)) where T : class, IDataModel;
        Task<int> Insert<T>(T entity, CancellationToken token = default(CancellationToken)) where T : class, IDataModel;
        Task<int> Delete<T>(object id, CancellationToken token = default(CancellationToken)) where T : class, IDataModel;
        Task<IEnumerable<T>> FromSqlRaw<T>(string sql, CancellationToken token = default(CancellationToken), params object[] parameters) where T : class;
    }
}
