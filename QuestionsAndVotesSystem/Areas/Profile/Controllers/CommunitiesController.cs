using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionsAndVotesSystem.Api.Model;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using QuestionsAndVotesSystem.Api.Controller;
using System.Net;


namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class CommunitiesController : SecurityController
    {
        private CommunitiesApiController data = new CommunitiesApiController();


        // GET: Profile/Comunities
        public ActionResult Index()
        {
            string lang = Request.Cookies.Get("languageCookie").Value;
            ViewBag.AlertMsg = TempData["msg"];

            CommunityVM community = new CommunityVM();
            community.communities = data.GetCommunityList(lang).ToList();
            return View(community);
        }

        // GET: Profile/Comunities/Details/5
        public ActionResult Details(int id)
        {
            return PartialView("_CommunityDetails", data.GetCommunity(id));
        }

        // GET: Profile/Comunities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Comunities/Create
        [HttpPost]
        public ActionResult Create(Community community)
        {
            try
            {
                if (data.PostCommunity(community))
                {
                    TempData["msg"]= "AlertMsg('success', 'New Community was added successfully');";
                    return RedirectToAction("Index");
                }
                TempData["msg"] = "AlertMsg('error', 'Failed to add new Community');";
                return View(community);
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Comunities/Edit/5
        public ActionResult Edit(int id)
        {
            Community com = new Community();
            com = data.GetCommunity(id);
            if (com == null)
            {
                TempData["msg"] = "AlertMsg('error', 'Community Not found');";
                return RedirectToAction("Index");
            }
            return View(com);
        }

        // POST: Profile/Comunities/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Community community)
        {
            if (data.PutCommunity(id, community))
            {
                TempData["msg"] = "AlertMsg('success', 'Community was updated successfully');";
                return RedirectToAction("Index");
            }
            TempData["msg"] = "AlertMsg('error', 'Failed to update Community');";
            return View("community");
        }

       

        // POST: Profile/Comunities/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            
                if (data.DeleteCommunity(int.Parse(id)))
                {

                    TempData["msg"] = "AlertMsg('success', 'Community was deleted successfully');";
                    return RedirectToAction("Index");
                }
                TempData["msg"] = "AlertMsg('error', 'Failed to add new Community');";
               

                return RedirectToAction("Index");
           
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                data.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
