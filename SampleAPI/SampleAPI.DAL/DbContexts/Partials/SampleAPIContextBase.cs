using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleAPI.DAL.Models;

namespace SampleAPI.DAL.DbContexts
{
    public abstract partial class SampleAPIContextBase : DbContext
    {
    }
}
