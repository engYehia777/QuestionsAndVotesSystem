using Microsoft.AspNet.Identity;
using QuestionsAndVotesSystem.Api.Controller;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using QuestionsAndVotesSystem.Areas.Profile.AuthorizeRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PostApiController apiData = new PostApiController();
        [AllowAnonymous]
        public ActionResult Index()
        {
            PostVM objVM = new PostVM();
            objVM.UserId = User.Identity.GetUserId();
            if (objVM.UserId == null || objVM.UserId == string.Empty)
            {
                objVM.indexQuestions = apiData.Get().ToList();
            }
            else
            {
                objVM.indexQuestions = apiData.GetByUserIdAndCommunity(objVM.UserId);
            }
            
            //return RedirectToAction("Index", "Post", new { Area = "Profile" });
            return View(objVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

     
    }
}