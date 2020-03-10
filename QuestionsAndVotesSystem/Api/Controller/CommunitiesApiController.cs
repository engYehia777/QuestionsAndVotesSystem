using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuestionsAndVotesSystem.Api.Model;
using QuestionsAndVotesSystem.Api.Model.Poco;


namespace QuestionsAndVotesSystem.Api.Controller
{
    public class CommunitiesApiController : ApiController
    {
        private DBEntities db = new DBEntities();

      
        // community List 
        public IEnumerable<CommunityPoco> GetCommunityList(string language)
        {
            return db.Communities.ToList().Select(c => new CommunityPoco(c, language));
            //return (language.Equals("ar-EG")) ? db.Communities.ToList().Select(c => new CommunityPoco() { Id = c.Id, Name = c.NameAr }).ToList() :
            //       db.Communities.ToList().Select(c => new CommunityPoco() { Id = c.Id, Name = c.NameEn }).ToList();
        }

        // GET: api/CommunitiesApi/5
        [ResponseType(typeof(Community))]
        public Community GetCommunity(int id)
        {
            Community community = db.Communities.Find(id);
           
            return community;
        }

        // PUT: api/CommunitiesApi/5
       // [ResponseType(typeof(void))]
        public bool PutCommunity(int id, Community community)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            if (id != community.Id)
            {
                return false;
            }

            db.Entry(community).State = EntityState.Modified;

            try
            {
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return false;
        }

        // POST: api/CommunitiesApi
        //[ResponseType(typeof(Community))]
        public bool PostCommunity(Community community)
        {
            if (!ModelState.IsValid)
            {
                return false;
                ///return  BadRequest(ModelState);
            }

            db.Communities.Add(community);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


            
        }

        // DELETE: api/CommunitiesApi/5
        //[ResponseType(typeof(Community))]
        public bool DeleteCommunity(int id)
        {
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return false;
            }

            db.Communities.Remove(community);
            if (db.SaveChanges() > 0)
            {
                return true;
            } 

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommunityExists(int id)
        {
            return db.Communities.Count(e => e.Id == id) > 0;
        }
    }
}