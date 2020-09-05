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

        public SampleAPIContextGeneric(DbContextOptions<SampleAPIContextBase> options) : base(options)
        {
        }

        public DbSet<T> Entities { get; set; }
    }
}
