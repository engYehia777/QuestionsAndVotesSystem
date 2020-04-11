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
            return db.Communities.ToList().Select(c => new CommunityPoco(c, language)).OrderBy(c => c.Name);
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

        // community List for spesific user 
        public IEnumerable<CommunityPoco> GetCommunityListForUser(string UID, string language)
        {
            return  db.Communities.ToList().Select(c => new CommunityPoco(c, UID, language)); 
            //Community obj;
            //List<CommunityPoco> ListOFComm = new List<CommunityPoco>();
            //var result = from c in db.Communities
            //             join u in db.User_Communities on c.Id equals u.CommunitieId into cu
            //             from communityAndUsers in cu.DefaultIfEmpty()
            //             select new
            //             {
            //                 Id = c.Id,
            //                 NameAr = c.NameAr,
            //                 NameEn = c.NameEn,
            //                 DescriptionAr = c.DescriptionAr,
            //                 DescriptionEn = c.DescriptionEn,
            //                 userId = communityAndUsers.UserId
            //             };
            //if (result != null)
            //{
            //    foreach (var item in result)
            //    {
            //        obj = new Community();
            //        obj.Id = item.Id;
            //        obj.NameAr = item.NameAr;
            //        obj.NameEn = item.NameEn;
            //        obj.DescriptionAr = item.DescriptionAr;
            //        obj.DescriptionEn = item.DescriptionEn;

                             //        ListOFComm.Add(new CommunityPoco(obj,item.userId , language));
                             //    }
                             //}
                             //return ListOFComm.OrderBy(c => c.Name);
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
           List<User_Communities> uc = db.User_Communities.Where(x => x.CommunitieId.Equals(id)).ToList(); 
            if (community == null)
            {
                return false;
            }

            db.Communities.Remove(community);
            db.User_Communities.RemoveRange(uc);
            try
            {
                db.SaveChanges();

                return true;
                
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                string err = e.Message;
                string innerEx = e.InnerException.ToString();
                return false;
            }
            

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


        public bool Follow(CommunityPoco community)
        {
            User_Communities UC = new User_Communities();
            UC.CommunitieId = community.Id;
            UC.UserId = community.userId;
            UC.FollowingDate = DateTime.Now;
            var cs = db.User_Communities.OrderByDescending(c => c.Sort).FirstOrDefault(u => u.UserId == community.userId).Sort;
            if (cs == null)
            {
                UC.Sort = 1;
            }
            else
            {
                UC.Sort = ++cs;
            }
            db.User_Communities.Add(UC);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UnFollow(CommunityPoco community)
        {
            User_Communities UC = new User_Communities();

            var cs = db.User_Communities.Single(uc => uc.CommunitieId == community.Id && uc.UserId == community.userId);
            db.User_Communities.Remove(cs);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}