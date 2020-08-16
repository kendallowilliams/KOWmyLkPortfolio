﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleAPI.DAL.DbContexts
{
    public partial class SampleAPIContextGeneric<T> : SampleAPIContext where T: class
    {
        public SampleAPIContextGeneric() : base()
        {
        }

        public SampleAPIContextGeneric(DbContextOptions<SampleAPIContext> options) : base(options)
        {
        }

        public DbSet<T> Entities { get; set; }
    }
}