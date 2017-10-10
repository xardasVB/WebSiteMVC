using Autofac;
using Autofac.Integration.Mvc;
using BLL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMVC
{
    public class AutofacConfig
    {
        public static void ConfigContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new DataModule("HotelDB"));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}