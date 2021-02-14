using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevTools.DAL.Services.Interfaces
{
    public interface IDataService
    {
        Task Insert<TDbContext, TEntity>(TEntity entity, CancellationToken token = default(CancellationToken))
               where TDbContext : DbContext, new()
               where TEntity : class;

        Task Insert<TDbContext, TEntity>(IEnumerable<TEntity> entity, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class;

        Task<bool> Delete<TDbContext, TEntity>(object id, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class;

        Task<TEntity> Get<TDbContext, TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetList<TDbContext, TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class;

        Task Update<TDbContext, TEntity>(TEntity entity, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class;

        Task ExecuteSqlRaw<TDbContext>(string sql, CancellationToken token = default(CancellationToken), params object[] parameters)
            where TDbContext : DbContext, new();

        Task<IEnumerable<TEntity>> FromSqlRaw<TDbContext, TEntity>(string sql, CancellationToken token = default(CancellationToken), params object[] parameters)
            where TDbContext : DbContext, new()
            where TEntity : class;
    }
}
