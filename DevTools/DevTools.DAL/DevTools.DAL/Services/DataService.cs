﻿using DevTools.DAL.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevTools.DAL.Services
{
    [Export(typeof(IDataService))]
    public class DataService : IDataService
    {
        private readonly int timeout;

        [ImportingConstructor]
        public DataService()
        {
            timeout = 7200;
        }

        public async Task Insert<TDbContext, TEntity>(TEntity entity, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            using (var db = new TDbContext())
            {
                DbSet<TEntity> set = db.Set<TEntity>();

                set.Add(entity);
                await db.SaveChangesAsync(token);
            }
        }

        public async Task Insert<TDbContext, TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            using (var db = new TDbContext())
            {
                DbSet<TEntity> set = db.Set<TEntity>();

                set.AddRange(entities);
                await db.SaveChangesAsync(token);
            }
        }

        public async Task<bool> Delete<TDbContext, TEntity>(object id, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            bool deleted = false;

            using (var db = new TDbContext())
            {
                DbSet<TEntity> set = null;
                TEntity entity = null;

                db.Database.SetCommandTimeout(timeout);
                set = db.Set<TEntity>();
                entity = await set.FindAsync(id);
                set.Remove(entity);
                deleted = await db.SaveChangesAsync(token) > 0;
            }

            return deleted;
        }

        public async Task Update<TDbContext, TEntity>(TEntity entity, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            using (var db = new TDbContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync(token);
            }
        }

        public async Task<TEntity> Get<TDbContext, TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            TEntity result = default(TEntity);

            using (var db = new TDbContext())
            {
                result = await (expression != null ? db.Set<TEntity>().Where(expression) : db.Set<TEntity>()).FirstOrDefaultAsync(token);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetList<TDbContext, TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken token = default(CancellationToken))
            where TDbContext : DbContext, new()
            where TEntity : class
        {
            IEnumerable<TEntity> results = Enumerable.Empty<TEntity>();

            using (var db = new TDbContext())
            {
                results = await (expression != null ? db.Set<TEntity>().Where(expression) : db.Set<TEntity>()).ToListAsync(token);
            }

            return results;
        }

        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedure<TDbContext, TEntity>(string sql,
                                                                                   CancellationToken token = default(CancellationToken),
                                                                                   params object[] parameters)
            where TDbContext : DbContext, new()
            where TEntity : class, new()
        {
            IEnumerable<TEntity> entities = Enumerable.Empty<TEntity>();

            using (var db = new TDbContext())
            {
            }

            return entities;
        }

        public async Task<IEnumerable<TEntity>> SqlQuery<TDbContext, TEntity>(string sql,
                                                                              CancellationToken token = default(CancellationToken),
                                                                              params object[] parameters)
            where TDbContext : DbContext, new()
            where TEntity : class, new()
        {
            IEnumerable<TEntity> entities = Enumerable.Empty<TEntity>();

            using (var db = new TDbContext())
            {
            }

            return entities;
        }

        public SqlParameter CreateParameter(string name, object value) => new SqlParameter(name, value);
    }
}
