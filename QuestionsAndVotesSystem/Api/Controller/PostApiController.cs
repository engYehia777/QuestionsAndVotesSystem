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
using System.Web.Mvc;

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


        }

        // GET: api/PostApi/5
        public PostPoco Get(int id)
        {
            var question = db.Questions.Single(q => q.Id == id);
            PostPoco poco = new PostPoco(question);
            return poco;

        }

        // this fn to get questions depend on communites user followed
        public List<PostPoco> GetByUserIdAndCommunity(string userId)
        {
            Question obj;
            List<PostPoco> ListOFQuestions = new List<PostPoco>();
            var result = from q in db.Questions
                         join qc in db.Questions_Comunities on q.Id equals qc.QuestionId
                         join uc in db.User_Communities on qc.ComunitieId equals uc.CommunitieId
                         where uc.UserId == userId

                         select new { q };
            if (result != null)
            {
                foreach (var item in result.Distinct().OrderByDescending(c => c.q.EndDate))
                {
                    obj = new Question();
                    obj.Id = item.q.Id;
                    obj.PostId = item.q.PostId;
                    obj.QuesetionTitle = item.q.QuesetionTitle;
                    obj.PhotoUrl = item.q.PhotoUrl;
                    obj.IsRequired = item.q.IsRequired;
                    obj.EndDate = item.q.EndDate;
                    obj.Answer_Types = item.q.Answer_Types;
                    obj.Question_Answer_Values = item.q.Question_Answer_Values;
                    obj.Questions_Comunities = item.q.Questions_Comunities;

                    ListOFQuestions.Add(new PostPoco(obj));
                }
            }
            return ListOFQuestions;
            //return db.Questions.OrderByDescending(q => q.EndDate).ToList().Select(q => new PostPoco(q));
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
            var answerImgs = viewModel.answerImgs;
            for (int a = 0; a < aValues.Length; a++)
            {
                var answerValues = new Question_Answer_Values();
                answerValues.OptionNum = a;
                answerValues.AnswerValue = aValues[a];
                if (answerImgs != null)
                {
                    var img = answerImgs[a];

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
                    string err = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
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


        public bool AnswerSubmit(PostVM VMobj)
        {
            User_Answers answers = new User_Answers();
            answers.QuestionId = VMobj.questionId;
            answers.UserId = VMobj.UserId;
            answers.SelectedAnswerIds = VMobj.answerValues;

            db.User_Answers.Add(answers);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    string err = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                string err = e.Message;
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
    }
}
