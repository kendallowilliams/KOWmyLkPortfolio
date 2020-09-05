using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
