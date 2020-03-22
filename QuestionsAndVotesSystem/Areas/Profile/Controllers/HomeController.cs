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

      
    }
}
