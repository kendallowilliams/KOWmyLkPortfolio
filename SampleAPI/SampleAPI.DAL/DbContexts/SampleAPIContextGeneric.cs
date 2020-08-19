using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleAPI.DAL.DbContexts
{
    public class SampleAPIContextGeneric<T> : SampleAPIContext where T: class
    {
        public SampleAPIContextGeneric() : base()
        {
        }

        public SampleAPIContextGeneric(DbContextOptions<SampleAPIContext> options) : base(options)
        {
        }

        public DbSet<T> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
