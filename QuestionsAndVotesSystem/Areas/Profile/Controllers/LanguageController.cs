using QuestionsAndVotesSystem.Api.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class LanguageController : Controller
    {

        private LanguageApiController data = new LanguageApiController();
        // GET: Profile/Language
        public ActionResult LanguageLinks()
        {
            return PartialView("_LanguageMenu",data.GetLanguage());
        }
    
        public ActionResult SetLanguage(string url, int id, string changTo)
        {
            bool isFound = data.LanguageExists(id);
            

            if (!string.IsNullOrEmpty(changTo) && isFound)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(changTo);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(changTo);

                HttpCookie LangCookie = new HttpCookie("languageCookie");
                LangCookie.Value = changTo;
                LangCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.SetCookie(LangCookie);
                //HttpContext.Response.Cookies.Add(LangCookie);
                
            }

            return Redirect(url);
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