using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QuestionsAndVotesSystem.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class UserController : SecurityController
    {
        private DBEntities db = new DBEntities();

        Error E ;
        // GET: Profile/User
        public ActionResult Index()
        {
            var aspnetusers = db.AspNetUsers;
            return View(aspnetusers.ToList());
        }

      

        // GET: Profile/User/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new MultiSelectList(db.AspNetRoles.Where(s => s.IsActive == true), "Name", "Name");
            return View();
        }

        // POST: Profile/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AspNetUser aspnetuser, string Password, string[] Roles)
        {
            ViewBag.Roles = new MultiSelectList(db.AspNetRoles.Where(s => s.IsActive == true), "Name", "Name");
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = new ApplicationUser
                {
                    IsActive = true,
                    UserName = aspnetuser.UserName,
                    Email=aspnetuser.Email,
                    PhoneNumber=aspnetuser.PhoneNumber
                };

                var result = await manager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    foreach (string Name in Roles)
                    {
                        await manager.AddToRoleAsync(user.Id, Name);

                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                        return View(aspnetuser);
                    }

                }

                return RedirectToAction("Index");
            }


            return View(aspnetuser);
        }

        // GET: Profile/User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("Error", E = new Error(1));
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);

            ViewBag.Roles = new MultiSelectList(db.AspNetRoles.Where(s => s.IsActive == true), "Name", "Name", aspnetuser.AspNetRoles.Where(s => s.IsActive == true).Select(s => s.Name).ToList());
            if (aspnetuser == null)
            {
                return View("Error", E = new Error(1));
            }

            return View(aspnetuser);
        }

        // POST: Profile/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AspNetUser aspnetuser, string[] Role)
        {
            ViewBag.Roles = new MultiSelectList(db.AspNetRoles.Where(s => s.IsActive == true), "Name", "Name", aspnetuser.AspNetRoles.Where(s => s.IsActive == true).Select(s => s.Name).ToList());
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = manager.Users.FirstOrDefault(x => x.Id == aspnetuser.Id);

                user.UserName = aspnetuser.UserName;
                user.IsActive = aspnetuser.IsActive;
                user.Email = aspnetuser.Email;
                user.PhoneNumber = aspnetuser.PhoneNumber;
                var result = await manager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    List<string> NeedToAdd = new List<string>();

                    List<string> NeedToDelet = new List<string>();
                    List<string> UserPrimtion = new List<string>();



                    var rolesForUser = await manager.GetRolesAsync(aspnetuser.Id);

                    foreach (string p in rolesForUser)
                    {
                        UserPrimtion.Add(p);

                    }
                    NeedToDelet =
                            UserPrimtion.Except(
                                  UserPrimtion.Where(
                                      x => Role.Contains((string)x)

                      )).ToList();
                    NeedToAdd = Role.Except(Role.Where(x => UserPrimtion.Select(s => s).Contains(x))).ToList();
                    foreach (string Name in NeedToDelet)
                    {
                        await manager.RemoveFromRoleAsync(aspnetuser.Id, Name);


                    }
                    foreach (string Name in NeedToAdd)
                    {
                        await manager.AddToRoleAsync(aspnetuser.Id, Name);
                    }


                    return RedirectToAction("Index");
                }
                else
                    return View(aspnetuser);
            }

            return View(aspnetuser);
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
