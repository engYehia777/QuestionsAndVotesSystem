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
using QuestionsAndVotesSystem.Areas.Profile.AuthorizeRole;

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

           // return PartialView("_Feed", postModel);
            return View(postModel);
        }

  
    

        // POST: Profile/Post/saveQuestion
        [HttpPost]
        public ActionResult Create(PostVM data)
        {
            bool isSave = false;
            
            
                data.UserId= User.Identity.GetUserId();
                data.PostEndDate = data.EndDate;
            if (data.questionId == 0 && data.PostId == 0)
            {
                data.CreationDate = DateTime.Now;
                isSave = apiData.Post(data);
            }
            else
            {
                //isSave = apiData.Update(data);
            }
            if (isSave)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Json(isSave, JsonRequestBehavior.AllowGet);
            }



            // return PartialView("_Create", data);
            
        }

     

        // GET: Profile/Post/Edit/5
        [HttpPost]
        public ActionResult Editpost(string id)
        {
          
            var question = apiData.Get(int.Parse(id));
            
            return Json(question, JsonRequestBehavior.AllowGet);
        }

        // POST: Profile/Post/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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
