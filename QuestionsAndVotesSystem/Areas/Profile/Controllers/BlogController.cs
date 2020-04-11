using QuestionsAndVotesSystem.Api.Controller;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class BlogController : SecurityController
    {
        private PostApiController apiData = new PostApiController();


        // GET: Profile/PostManager
        public ActionResult Index()
        {
            PostVM objVM = new PostVM();
            objVM.indexQuestions = apiData.GetAnsweredQuestions();

            return View(objVM);
        }

        // GET: Profile/PostManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/PostManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/PostManager/Create
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

        // GET: Profile/PostManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/PostManager/Edit/5
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

        // GET: Profile/PostManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/PostManager/Delete/5
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
