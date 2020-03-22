using Microsoft.AspNet.Identity;
using QuestionsAndVotesSystem.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Controllers
{
    public class authorizationController : Controller
    {
        private DBEntities db;
        public authorizationController()
        {
            db = new DBEntities();
        }

        public PartialViewResult Menu()
        {
            string userId = User.Identity.GetUserId();
            var pages = db.UserPages(userId).ToList();
            return PartialView(pages);
        }


        public PartialViewResult Links(string URl, string ID, string type, string name = "")
        {

            Links l = new Links();
            l.URL = URl;
            l.ID = ID;
            l.name = name;
            l.type = type;
            if (ID == "")
                l.Action = "onclick = OpenAddNew('" + URl + "')";
            else
                l.Action = "href ='" + URl + "'";

            if (type == "A")
                return PartialView("AddPremission", l);
            else if (type == "U")
                return PartialView("EditePremission", l);
            else if (type == "E")
                return PartialView("DetailsPremission", l);
            else
                return PartialView("DeletePremission", l);

        }


        public ActionResult DeleteLink(int id)
        {
            if (Session["CanDelete"] != null)
            {

                if (Session["CanDelete"].Equals(true))
                {
                    return Content("<button type='button' id='del' class='btn btn-default ICON' data-id="+ id +" data-toggle='modal' data-target='#DeleteModal'><i class='glyphicon glyphicon-trash'></i> Delete</button>");
                   // return Content("<a class='btn btn-default ICON' href='" + URl + "' <span class='glyphicon glyphicon-trash' title='Delete'/> </span> Delete</a>", "text/html");
                }
                return Content("");
            }
            return Content("");
        }

    }
}