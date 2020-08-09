using SampleAPI.API.DependencyResolvers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SampleAPI.API
{
    public class MefConfig
    {
        public static void RegisterMef(HttpConfiguration config)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            DirectoryCatalog catalog = new DirectoryCatalog(path);
            CompositionContainer container = new CompositionContainer(catalog);
            MefDependencyResolver resolver = new MefDependencyResolver(container);

            config.DependencyResolver = resolver;
        }
    }
}