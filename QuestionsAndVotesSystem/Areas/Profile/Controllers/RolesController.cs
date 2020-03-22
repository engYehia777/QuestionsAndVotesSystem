using QuestionsAndVotesSystem.Api.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class RolesController : SecurityController
    {
        private DBEntities db;
        Error E = new Error(1);
        public RolesController()
        {
            db = new DBEntities();
        }
        // GET: Profile/Roles
        public ActionResult Index()
        {
            return View(db.AspNetRoles.ToList());
        }

      

        // GET: Profile/Roles/Create
        public ActionResult Create()
        {
            MVRolesSetting RS = new MVRolesSetting();
            RS = RS.CreateNew(RS);
            return View(RS);
        }

        // POST: Profile/Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MVRolesSetting MVRole)
        {
            if (ModelState.IsValid)
            {
                MVRole.Role.IsActive = true;

                db.AspNetRoles.Add(MVRole.Role);

                db.Roleprimissions.AddRange(MVRole.Pages);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        string err = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            string errr = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);

                           throw new Exception(errr);
                        }
                    }
                   
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    string err = e.Message;
                   
                }

                return RedirectToAction("Index");
            }

            return View(MVRole);
        }

        // GET: Profile/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                HandleErrorInfo handeError = new HandleErrorInfo(new Exception(E.Message), "Roles", "Edit");
                return View("Error", handeError);
            }
            MVRolesSetting RS = new MVRolesSetting();
            RS = RS.Update(id);

            return View(RS);
        }

        // POST: Profile/Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Edit(MVRolesSetting MVRole)
        {
            var roles = db.AspNetRoles.FirstOrDefault(s => s.Id == MVRole.Role.Id);
            roles.IsActive = MVRole.Role.IsActive;
            roles.Name = MVRole.Role.Name;
            db.Entry(roles).State = EntityState.Modified;
            var deletePrev = db.Roleprimissions.Where(s => s.RoleId == roles.Id).ToList();
            db.Roleprimissions.RemoveRange(deletePrev);
            db.Roleprimissions.AddRange(MVRole.Pages);
            db.SaveChanges();
            return RedirectToAction("Index");
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
