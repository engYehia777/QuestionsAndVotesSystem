using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Profile_default",
                "Profile/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "QuestionsAndVotesSystem.Areas.Profile.Controllers" }
            );
        }
    }
}