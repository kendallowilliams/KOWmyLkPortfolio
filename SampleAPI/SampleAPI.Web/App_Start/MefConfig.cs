using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleAPI.Web.ControllerFactories;

namespace SampleAPI.Web.App_Start
{
    public class MefConfig
    {
        public static void RegisterMEF(ControllerBuilder builder)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            builder.SetControllerFactory(new MefControllerFactory(path));
        }
    }
}