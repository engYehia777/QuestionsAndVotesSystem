using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile.Helper
{
    public static class ClassHelper
    {
        public static string Links( this HtmlHelper helper, string target, string Classes, string TextLable, string action, int type)
        {
            if (type == 1)
            {
                return String.Format("<a target='{0}'  class='{1} ICON' {3}>{2}  <span class='glyphicon glyphicon-plus' title='Add'> </span> New</a>", target, "btn btn-primary", "", action);
            }
            else if (type == 2)
            {
                return String.Format("<a target='{0}'  class='{1} ICON' {3}>{2}  <span class='glyphicon glyphicon-pencil' title='Edit'> </span> Edit</a>", target, "btn btn-default", "", action);
            }
            else if (type == 3)
            {
                return String.Format("<a target='{0}'  class='{1} ICON' {3}>{2}  <span class='glyphicon glyphicon-share' title='Details'> </span> Details</a>", target, "btn btn-info", "", action);
            }
            else if (type == 5)
            {
                return String.Format("<a target='{0}'  class='{1} ICON' {3}>{2}  <span class='glyphicon glyphicon-trash' title='Delete'> </span> Delete</a>", target, "btn btn-danger", "", action);
            }
            else if (type == 6)
            {
                return String.Format("<a target='{0}'{1}>Dashboard</a>", target, action);
            }

            else
            {
                return String.Format("<a target='{0}'  class='{1} ICON' {3}>{2}</a>", target, "btn-link", TextLable, action);
            }
        }
    }
}