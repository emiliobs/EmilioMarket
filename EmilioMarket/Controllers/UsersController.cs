using EmilioMarket.Models;
using EmilioMarket.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmilioMarket.Controllers
{
    public class UsersController : Controller
    {

       private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                { 
                    Email = user.Email,
                    Name = user.UserName,
                    UserId = user.Id
                };

                usersView.Add(userView);
            }
            return View(usersView);
        }

        public ActionResult Roles(string userID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var user = users.Find(u => u.Id == userID);

            var rolesView = new List<RoleView>();

            
                foreach (var item in user.Roles)
                {
                    var role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleId = role.Id,
                        Name = role.Name
                    };

                    rolesView.Add(roleView);
                }
            
            

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
                Roles = rolesView 
            };

            return View(userView);  
        }

      [HttpGet]
         public ActionResult AddRole(string userId)
         {

            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
              Email = user.Email,
              Name = user.UserName,
              UserId = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //para mandar la lista a los Roles:
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Select a Role.....]" });
            ViewBag.RoleId = new SelectList(list.OrderBy(r => r.Name), "Id", "Name");

            return View(userView);  
         }

         [HttpPost]
        public ActionResult AddRole(string userId, FormCollection form)
        {
            var roleId = Request["RoleId"];

            //Para buscar el nombre del role:
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userId);


            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id
            };

            if (string.IsNullOrEmpty(roleId))
            {
                ViewBag.Error = "You must Select a Role.";

               
                //para mandar la lista a los Roles:
                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Select a Role.....]" });
                ViewBag.RoleId = new SelectList(list.OrderBy(r => r.Name), "Id", "Name");
                return View(userView);
            }

            //lista de roles:
            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleId);

            if (!userManager.IsInRole(userId, role.Name))
            {
                userManager.AddToRole(userId, role.Name);
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                
                var roleView = new RoleView
                {
                    Name = role.Name,
                    RoleId = role.Id    
                };

                rolesView.Add(roleView);
            }

            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserId = user.Id    
            };

           

            return View("Roles", userView);
        }

        //Delete
        public ActionResult Delete(string userId, string roleId)
        {
            //Validar los para metros:
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.Users.ToList().Find(u => u.Id == userId);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleId);

            //Delte user from role:
            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            //Prepare the view to return:
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    Name = role.Name,
                    RoleId = role.Id
                         
                };

                rolesView.Add(roleView);
            }

            var userView = new UserView
            { 
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserId = user.Id
            };

            return View("Roles", userView);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}