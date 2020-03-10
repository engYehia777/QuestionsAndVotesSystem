using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionsAndVotesSystem.Api.Controller;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using System.IO;
using Microsoft.AspNet.Identity;
using QuestionsAndVotesSystem.Api.Model.Poco;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class PostController : SecurityController
    {
        private PostApiController apiData = new PostApiController();
        

        // GET: Profile/Post
        public ActionResult Index()
        {
            string lang = Request.Cookies.Get("languageCookie").Value;
            CommunitiesApiController communitydata = new CommunitiesApiController();
            PostVM postModel = new PostVM();
            postModel.communities = new SelectList(communitydata.GetCommunityList(lang), "Id", "Name");
            postModel.answerTypes = new SelectList(apiData.GetAnswerTypesList(lang), "answerType", "answerTypeName");
            postModel.indexQuestions = apiData.Get().ToList();
            

            return View(postModel);
        }

  
    

        // POST: Profile/Post/saveQuestion
        [HttpPost]
        public ActionResult Create(PostVM data)
        {
            bool isSave = true;
            
            
                data.UserId= User.Identity.GetUserId();
                data.CreationDate = DateTime.Now;
                data.PostEndDate = data.EndDate;
                //isSave = apiData.Post(data);
            
            if (isSave)
            {
                return PartialView("_PostItem", data);
            }
           
                return Json(new { error = "errorrrr" });
            
        }

        // GET: Profile/Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Post/Edit/5
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

        // GET: Profile/Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Post/Delete/5
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
