using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.DAL.DbContextConfigs
{
    public static class SampleAPIContextConfig
    {
        /// <summary>
        /// <para>Contains the entity types to be included in SampleAPIContext model. Default: all entities included.</para>
        /// <para>Needs to be set before first use of SampleAPIContext.</para>
        /// </summary>
        public static IEnumerable<Type> Entities { get; set; }
    }
}
