using QuestionsAndVotesSystem.Api.Model;
using QuestionsAndVotesSystem.Api.Model.Poco;
using QuestionsAndVotesSystem.Api.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace QuestionsAndVotesSystem.Api.Controller
{
    public class PostApiController : ApiController
    {

        private DBEntities db = new DBEntities();

        // rturn answer types List
        public IEnumerable<PostPoco> GetAnswerTypesList(string language)
        {
            return db.Answer_Types.ToList().Select(c => new PostPoco(c, language));
            
        }

        // questions list
        public IEnumerable<PostPoco> Get()
        {
            return db.Questions.OrderByDescending(q => q.EndDate).ToList().
                Select(q => new PostPoco(q));

          //return  from q in db.Questions
          //  join a in db.Question_Answer_Values on q.Id equals a.QuestionId
          //  join t in db.Answer_Types on q.AnswerTypeId equals t.Id
          //  where q.EndDate >= DateTime.Now
          //  select new PostPoco()
          //  {
          //      QuesetionTitle = q.QuesetionTitle,
          //      QPhotoFile = q.PhotoUrl,
          //      IsRequired = q.IsRequired,
          //      EndDate = q.EndDate,
          //      answerType = t.Type,
          //      answerValues= q.Question_Answer_Values.ToList()

                
          //  };
        }

        // GET: api/PostApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PostApi
        public bool Post(PostVM viewModel)
        {
            var user = db.AspNetUsers.Find(viewModel.UserId);
           // file Post info
            var Post_Info = new Post_Info();
            Post_Info.CreationDate = viewModel.CreationDate;
            Post_Info.EndDate = viewModel.PostEndDate;
            Post_Info.UserId = viewModel.UserId;
            Post_Info.AspNetUser = user;

            // fill question info
            var Question = new Question();
            var questionImg = viewModel.QPhotoFile;
            if (questionImg != null)
            {
                var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(questionImg.FileName);
                var Extention = Path.GetExtension(questionImg.FileName);
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileNameWithoutExtention + Extention;
                //questionImg.SaveAs(HttpContext.Current.Server.MapPath("/Api/UploadedImages/" + fileName));
                questionImg.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("/Api/UploadedImages/" + fileName));
                Question.PhotoUrl = "/Api/UploadedImages/" + fileName;
            }
            
            Question.AnswerTypeId = db.Answer_Types.Where(t => t.Type == viewModel.AnswerType).FirstOrDefault().Id;
            Question.QuesetionTitle = viewModel.QuesetionTitle;
            Question.IsRequired = viewModel.IsRequired;
            Question.EndDate = viewModel.EndDate;
            Question.IsRankeditorChoice = viewModel.IsRankeditorChoice;

            

            //fill question answers
            string[] aValues = viewModel.answerValues.Split(new Char[] { ';' });
            for (int a = 0; a < aValues.Length; a++)
            {
                var answerValues = new Question_Answer_Values();
                answerValues.OptionNum = a;
                answerValues.AnswerValue = aValues[a];
                if (viewModel.answerImgs.Count > 0)
                {
                    var img = viewModel.answerImgs[a];

                    var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(img.FileName);
                    var Extention = Path.GetExtension(img.FileName);
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileNameWithoutExtention + Extention;
                    //questionImg.SaveAs(HttpContext.Current.Server.MapPath("/Api/UploadedImages/" + fileName));
                    img.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("/Api/UploadedImages/" + fileName));
                    answerValues.PhotoUrl = "/Api/UploadedImages/" + fileName;

                }
                //fill question answers to question
                Question.Question_Answer_Values.Add(answerValues);
            }

            //fill question community
            string[] communiyIds = viewModel.ComunitieIds.Split(new Char[] { ';' });
            for (int c = 0; c < communiyIds.Length; c++)
            {
                var qCommunity = new Questions_Comunities();
                qCommunity.ComunitieId = int.Parse(communiyIds[c]);
                qCommunity.AddingDate = DateTime.Now;
                //fill question community into question
                Question.Questions_Comunities.Add(qCommunity);
            }


            
            //fill question into the post
            Post_Info.Questions.Add(Question);

            db.Post_Info.Add(Post_Info);
            try
            {
               db.SaveChanges();
                
                    return true;
                
                
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    string err= string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        string errr = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        throw new Exception(errr);
                    }
                }
                return false;
            }
            catch(System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                string err = e.Message;
                return false;
            }
           
            
        }

        // PUT: api/PostApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PostApi/5
        public void Delete(int id)
        {
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
