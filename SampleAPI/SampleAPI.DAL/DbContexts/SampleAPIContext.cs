using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleAPI.DAL.DbContextConfigs;
using SampleAPI.DAL.Models;

namespace SampleAPI.DAL.DbContexts
{
    public class SampleAPIContext : SampleAPIContextBase
    {
        public SampleAPIContext()
        {
        }

        public SampleAPIContext(DbContextOptions<SampleAPIContextBase> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Type> ignoredEntities = Enumerable.Empty<Type>();

            if (SampleAPIContextConfig.Entities == null) /*then*/ SampleAPIContextConfig.Entities = Enumerable.Empty<Type>();
            base.OnModelCreating(modelBuilder);
            if (SampleAPIContextConfig.Entities.Any())
            {
                ignoredEntities = modelBuilder.Model.GetEntityTypes()
                                                    .Where(item => !SampleAPIContextConfig.Entities.Contains(item.ClrType))
                                                    .Select(item => item.ClrType)
                                                    .ToList();
            }

            foreach(Type entity in ignoredEntities) { modelBuilder.Ignore(entity); }
        }
    }
}
