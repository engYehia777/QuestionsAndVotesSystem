using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using QuestionsAndVotesSystem.Api.Model;

namespace QuestionsAndVotesSystem.Areas.Profile.AuthorizeRole
{
    public class AuthorizeRolesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            int TypeID = ((actionName.ToLower().Contains("index") || actionName == "" || actionName.ToLower().Contains("details")) ? 4
                        : (actionName.ToLower().Contains("create") ? 1
                        : (actionName.ToLower().Contains("edit") ? 2
                        : (actionName.ToLower().Contains("delete") ? 3
                        : 0))));
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Replace("Controller", "");
            
            string IDUser = HttpContext.Current.User.Identity.GetUserId();
            DBEntities db = new DBEntities();

            var r = db.UserSecurityPages(IDUser, controllerName).ToList();
            HttpContext.Current.Session["CanDelete"] = r.FirstOrDefault(s => s.CanDelete == true) != null ? true : false;
            HttpContext.Current.Session["CanADD"] = r.FirstOrDefault(s => s.CanADD == true) != null ? true : false;
            HttpContext.Current.Session["CanUpdate"] = r.FirstOrDefault(s => s.CanUpdate == true) != null ? true : false;
            HttpContext.Current.Session["CanShow"] = r.FirstOrDefault(s => s.CanShow == true) != null ? true : false;
            var RoleWithType = r.FirstOrDefault(s => ((TypeID == 1 && s.CanADD == true)
                    || (TypeID == 2 && s.CanUpdate == true)
                    || (TypeID == 3 && s.CanDelete == true)
                    || (TypeID == 4 && s.CanShow == true)));
            if (RoleWithType != null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            filterContext.Result = new ViewResult() { ViewName = "NotAuth" };
            return;

        }
    }
}