using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using QLHS.Patterns.Repository;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Unity;
using Unity.AspNet.Mvc;

namespace QLHS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterServices()
        {
            // ??ng ký các d?ch v? ? ?ây
            var container = new UnityContainer();
            container.RegisterType<IGradeRepository, GradeRepository>();

            // ??ng ký DbContext QLDEntities
            container.RegisterType<QLDEntities>(new Unity.Lifetime.HierarchicalLifetimeManager());

            // Thi?t l?p Resolver cho MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
