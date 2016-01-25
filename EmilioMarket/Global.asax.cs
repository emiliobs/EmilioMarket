using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
//
using EmilioMarket.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmilioMarket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.EmilioMarketContext, Migrations.Configuration>());
            
            //Me conecto a las tablas de seguridad a la base de datos:
            ApplicationDbContext db = new ApplicationDbContext ();
            //Inicio Método Crear Roles:
            CreateRoles(db);
            //Método para crear SuperUsuarios:
            CreateSuperUser(db);
            //Método para adiccionar permisos al superUser:
            AddPermissionToSuperUser(db);
            //Close DB:
            db.Dispose();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissionToSuperUser(ApplicationDbContext db)
        {
            //Variable que nos permiter manipular los usuarios, crear, elimina y mas:
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            // Aquí ya el usuario existe:
            var user = userManager.FindByName("emiliobarrerasepulveda@gmail.com");

            //Verifico si el usuario ya tienes roles asignados, si no, se lo asigno :
            if (!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }

            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }

            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }

            if (!userManager.IsInRole(user.Id, "Details"))
            {
                userManager.AddToRole(user.Id, "Details");
            }

            if (!userManager.IsInRole(user.Id, "Orders"))
            {
                userManager.AddToRole(user.Id, "Orders");
            }

            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!userManager.IsInRole(user.Id, "Inventories"))
            {
                userManager.AddToRole(user.Id, "Inventories");
            }

            if (!userManager.IsInRole(user.Id, "Shopping"))
            {
                userManager.AddToRole(user.Id, "Shopping"); 
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            //Variable que nos permiter manipular los usuarios, crear, elimina y mas:
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //vaiable del rol manager   ue me permite administrar los roles:
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Usuarios:
            //Verifico si el usuario existe:
            var user = userManager.FindByName("emiliobarrerasepulveda@gmail.com");
            if (user == null)
            {
            //Si no existe el usuraio lo creo..
                user = new ApplicationUser
                {
                    UserName = "emiliobarrerasepulveda@gmail.com",
                    Email = "emiliobarrerasepulveda@gmail.com"
                };

                userManager.Create(user, "Emilio++55");
            }
        }

        //Método crear Roles:
        private void CreateRoles(ApplicationDbContext db)
        {
            //vaiable del roles manager   ue me permite administrar los roles:
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Roles:
            //Pregunto si existe el rol view, si no existo lo creo:
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }

            if (!roleManager.RoleExists("Details"))
            {
                roleManager.Create(new IdentityRole("Details"));
            }

            if (!roleManager.RoleExists("Orders"))
            {
                roleManager.Create(new IdentityRole("Orders"));
            }

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Inventories"))
            {
                roleManager.Create(new IdentityRole("Inventories"));
            }

            if (!roleManager.RoleExists("Shopping"))
            {
                roleManager.Create(new IdentityRole("Shopping"));
            }
        }
    }
}
