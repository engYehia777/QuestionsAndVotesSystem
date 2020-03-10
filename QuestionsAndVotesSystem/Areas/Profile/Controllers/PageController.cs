using QuestionsAndVotesSystem.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class PageController : SecurityController
    {
        private DBEntities db;
        Error E ;
        public PageController()
        {
            db = new DBEntities();
        }
        // GET: Profile/Page
        public ActionResult Index()
        {
            return View(db.Pages.ToList());
        }

        // GET: Profile/Page/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Page/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.Pages.Where(s => s.ParentID == null && s.IsActive == true), "ID", "Name");
            return View();
        }

        // POST: Profile/Page/Create
        [HttpPost]
        public ActionResult Create(Page page)
        {
            ViewBag.ParentID = new SelectList(db.Pages.Where(s => s.ParentID == null && s.IsActive == true), "ID", "Name");
            if (ModelState.IsValid)
            {
                page.IsActive = true;
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(page);
        }

        // GET: Profile/Page/Edit/5
        public ActionResult Edit(int id)
        {
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                E = new Error(1);
                HandleErrorInfo handeError = new HandleErrorInfo(new Exception(E.Message), "Roles", "Edit");
                return View("Error",handeError);
            }
            ViewBag.ParentID = new SelectList(db.Pages.Where(p => p.ParentID == null && p.IsActive == true), "ID", "Name", page.ParentID);
            return View(page);
        }

        // POST: Profile/Page/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Page page)
        {

            if (ModelState.IsValid)
            {
                db.Entry(page).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Pages.Where(s => s.ParentID == null && s.IsActive == true), "ID", "Name", page.ParentID);
            return View(page);

        }

        // GET: Profile/Page/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Page/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
