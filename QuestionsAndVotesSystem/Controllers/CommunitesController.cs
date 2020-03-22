using Microsoft.AspNet.Identity;
using QuestionsAndVotesSystem.Api.Controller;
using QuestionsAndVotesSystem.Api.Model.Poco;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using QuestionsAndVotesSystem.Areas.Profile.AuthorizeRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Controllers
{
    public class CommunitesController : Controller
    {
        private CommunitiesApiController data = new CommunitiesApiController();
        // GET: Communites
        [AllowAnonymous]
        public ActionResult AllCommunites()
        {
            string lang = Request.Cookies.Get("languageCookie").Value;
           

            CommunityVM community = new CommunityVM();
            community.communities = data.GetCommunityListForUser(lang).ToList();
            community.userId = User.Identity.GetUserId();
            return View(community);
        }

        [HttpPost]
        [AjaxAuthorize]
        public ActionResult Follow(CommunityPoco comunityInfo)
        {
            bool result = data.Follow(comunityInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAuthorize]
        public ActionResult UnFollow(CommunityPoco comunityInfo)
        {
            bool result = data.UnFollow(comunityInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
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