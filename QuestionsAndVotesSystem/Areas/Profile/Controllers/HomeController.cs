using QuestionsAndVotesSystem.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class HomeController : SecurityController
    {
        private DBEntities db;
        public HomeController()
        {
            db = new DBEntities();
        }
        // GET: Profile/Home
        
        public ActionResult Index()
        {

            return View();
        }

        // GET: Profile/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Home/Delete/5
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
    }
}
