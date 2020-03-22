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
    public class PostMVCController : Controller
    {
        private PostApiController data = new PostApiController();

        [AjaxAuthorize]
        [HttpPost]
        public ActionResult AnswerSubmit(PostVM VMobj)
        {
            bool result = data.AnswerSubmit(VMobj);
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