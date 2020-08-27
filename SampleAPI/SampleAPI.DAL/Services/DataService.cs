using SampleAPI.DAL.DbContexts;
using SampleAPI.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.EntityFrameworkCore;
using SampleAPI.DAL.Models.Interfaces;

namespace SampleAPI.DAL.Services
{
    [Export(typeof(IDataService))]
    public class DataService : IDataService
    {
        private int timeout;

        [ImportingConstructor]
        public DataService()
        {
            timeout = 120;
        }

        public async Task<IEnumerable<T>> GetList<T>(Expression<Func<T, bool>> expression = null, 
                                                     CancellationToken token = default, 
                                                     params Expression<Func<T, object>>[] includes) where T : class, IDataModel
        {
            IEnumerable<T> results = Enumerable.Empty<T>();

            using (var db = new SampleAPIContext())
            {
                IQueryable<T> query = db.Set<T>();

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.SetCommandTimeout(timeout);

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                results = await (expression != null ? query.Where(expression) : query).ToListAsync(token);
            }

            return results;
        }

        public async Task<IEnumerable<T>> GetListAlt<T>(Expression<Func<T, bool>> expression = null,
                                                     CancellationToken token = default,
                                                     params string[] includes) where T : class, IDataModel
        {
            IEnumerable<T> results = Enumerable.Empty<T>();

            using (var db = new SampleAPIContext())
            {
                IQueryable<T> query = db.Set<T>();

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.SetCommandTimeout(timeout);

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                results = await (expression != null ? query.Where(expression) : query).ToListAsync(token);
            }

            return results;
        }

        public async Task<int> Delete<T>(object id, CancellationToken token) where T : class, IDataModel
        {
            int result = default(int);

            using (var db = new SampleAPIContext())
            {
                DbSet<T> set = null;
                T entity = null;

                db.Database.SetCommandTimeout(timeout);
                set = db.Set<T>();
                entity = await set.FindAsync(id);
                set.Remove(entity);
                result = await db.SaveChangesAsync(token);
            }

            return result;
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression, CancellationToken token, params Expression<Func<T, object>>[] includes) where T : class, IDataModel
        {
            T result = default(T);

            using (var db = new SampleAPIContext())
            {
                IQueryable<T> query = db.Set<T>();

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.SetCommandTimeout(timeout);

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                result = await (expression != null ? query.Where(expression) : query).FirstOrDefaultAsync(token);
            }

            return result;
        }

        public async Task<T> GetAlt<T>(Expression<Func<T, bool>> expression, CancellationToken token, params string[] includes) where T : class, IDataModel
        {
            T result = default(T);

            using (var db = new SampleAPIContext())
            {
                IQueryable<T> query = db.Set<T>();

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.SetCommandTimeout(timeout);

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                result = await (expression != null ? query.Where(expression) : query).FirstOrDefaultAsync(token);
            }

            return result;
        }

        public async Task<int> Insert<T>(T entity, CancellationToken token) where T : class, IDataModel
        {
            int result = default(int);

            using (var db = new SampleAPIContext())
            {
                db.Database.SetCommandTimeout(timeout);
                entity.ModifiedOn = DateTime.Now;
                entity.CreatedOn = DateTime.Now;
                db.Set<T>().Add(entity);
                result = await db.SaveChangesAsync(token);
            }

            return result;
        }

        public async Task<int> Update<T>(T entity, CancellationToken token) where T : class, IDataModel
        {
            int result = default(int);

            using (var db = new SampleAPIContext())
            {
                db.Database.SetCommandTimeout(timeout);
                entity.ModifiedOn = DateTime.Now;
                db.Entry(entity).State = EntityState.Modified;
                result = await db.SaveChangesAsync(token);
            }

            return result;
        }

        public async Task<IEnumerable<T>> FromSqlRaw<T>(string sql, CancellationToken token = default(CancellationToken), params object[] parameters) where T : class
        {
            IEnumerable<T> entities = Enumerable.Empty<T>();

            using (var db = new SampleAPIContextGeneric<T>())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                entities = await db.Entities.FromSqlRaw(sql, parameters).ToListAsync(token);
            }

            return entities;
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> expression, CancellationToken token = default(CancellationToken)) where T : class
        {
            int count = 0;

            using (var db = new SampleAPIContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                count = await db.Set<T>().CountAsync(expression, token);
            }

            return count;
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> expression, CancellationToken token = default(CancellationToken)) where T : class
        {
            bool exists;

            using (var db = new SampleAPIContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                exists = await db.Set<T>().CountAsync(expression, token) > 0;
            }

            return exists;
        }
    }
}
