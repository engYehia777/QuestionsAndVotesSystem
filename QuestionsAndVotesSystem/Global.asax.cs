using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QuestionsAndVotesSystem.Api.Controller;
using QuestionsAndVotesSystem.Api.Model;
using System.Threading;
using System.Globalization;

namespace QuestionsAndVotesSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
            LanguageApiController apicontroler = new LanguageApiController();
            HttpCookie langCookie;
            langCookie = HttpContext.Current.Request.Cookies["languageCookie"];
            if (langCookie == null || langCookie.Value == null)
            {
                Language defaultLangInfo = new Language();
                defaultLangInfo = apicontroler.DefaultLanguage();

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(defaultLangInfo.Name);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLangInfo.Name);

                HttpCookie LangCookie = new HttpCookie("languageCookie");
                LangCookie.Value = defaultLangInfo.Name;
                
                Response.Cookies.Add(LangCookie);
            }
        }
    }
}
