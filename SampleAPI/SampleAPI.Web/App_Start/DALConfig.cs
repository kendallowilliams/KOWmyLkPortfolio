using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleAPI.DAL.DbContexts;
using SampleAPI.DAL.DbContextConfigs;
using SampleAPI.DAL.Models;

namespace SampleAPI.Web.App_Start
{
    public class DALConfig
    {
        public static void RegisterDAL()
        {
            SampleAPIContextConfig.Entities = new List<Type>() { };
        }
    }
}