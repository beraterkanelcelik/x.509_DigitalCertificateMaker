using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MainProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            using (Context context = new Context())
            {
                if (context.UserRoles.Count() == 0)
                {
                    var role1 = new UserRole();
                    var role2 = new UserRole();
                    role1.RoleName = "User";
                    role2.RoleName = "Admin";
                    context.UserRoles.Add(role1);
                    context.UserRoles.Add(role2);
                    context.SaveChanges();

                }
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
