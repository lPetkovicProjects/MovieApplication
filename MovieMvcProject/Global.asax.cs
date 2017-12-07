using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MovieMvcProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var context = new ApplicationDbContext();
            if (!context.Users.Any(user => user.UserName == "superadmin@a.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var applicationUser = new ApplicationUser() { UserName = "superadmin@a.com" };
                userManager.Create(applicationUser, "admin123");

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Admin"));

                userManager.AddToRole(applicationUser.Id, "Admin");
            }

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
