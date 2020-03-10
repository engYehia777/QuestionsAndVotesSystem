using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuestionsAndVotesSystem.Api.Model;
using QuestionsAndVotesSystem.Areas.Profile.AuthorizeRole;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    [Authorize]
    [AuthorizeRoles]
    public class SecurityController : Controller
    {
        private DBEntities db;

        public SecurityController()
        {
            db = new DBEntities();
        }

        public string IDU
        {
            get { return User.Identity.GetUserId(); }
            set { IDU = value; }
        }
        public DateTime CurrentDate
        {
            get { return DateTime.Now; }
            set { CurrentDate = value; }
        }
        public bool CanDelete(string Controle, int? Item)
        {
            // بتعمل ايه egyzz

            //var vv = db.checkeBeforeDelete(Controle, Item);
            //return db.checkeBeforeDelete(Controle, Item) == 1 ? true : false;

            return true;

        }
    }
}