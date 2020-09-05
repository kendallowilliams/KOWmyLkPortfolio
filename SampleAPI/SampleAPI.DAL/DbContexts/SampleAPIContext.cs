using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleAPI.DAL.Models;

namespace SampleAPI.DAL.DbContexts
{
    public class SampleAPIContext : SampleAPIContextBase
    {
        public static IEnumerable<string> Tables = Enumerable.Empty<string>();

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

            if (Tables == null) /*then*/ Tables = Enumerable.Empty<string>();
            base.OnModelCreating(modelBuilder);
            if (Tables.Any())
            {
                ignoredEntities = modelBuilder.Model.GetEntityTypes()
                                                    .Where(item => !Tables.Contains(item.Name, StringComparer.OrdinalIgnoreCase))
                                                    .Select(item => item.ClrType);
            }

            foreach(Type entity in ignoredEntities) { modelBuilder.Ignore(entity); }
        }
    }
}
