using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuestionsAndVotesSystem.Api.Model;
using System.Web.Http.Description;
using System.Web.Mvc;


namespace QuestionsAndVotesSystem.Api.Controller
{
    public class LanguageApiController : ApiController
    {
        private DBEntities db = new DBEntities();

        public IEnumerable<Language> GetLanguage()
        {
            return db.Languages.ToList();
        }


        [ResponseType(typeof(Language))]
        public Language GetLanguage(int id)
        {
            Language Language = db.Languages.Find(id);

            return Language;
        }


        public Language DefaultLanguage()
        {
            return db.Languages.Where(l => l.IsDefault == true).SingleOrDefault();
        }


        public bool LanguageExists(int id)
        {
            return db.Languages.Count(e => e.LanguageID == id && e.Show == true) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
