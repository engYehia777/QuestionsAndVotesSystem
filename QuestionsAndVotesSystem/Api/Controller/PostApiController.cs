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

        public bool QuestionLike(int questionId, string userId)
        {
            var likeInfo = new User_Likes();
            likeInfo.QuestionId = questionId;
            likeInfo.UserId = userId;
            db.User_Likes.Add(likeInfo);
            var question = db.Questions.FirstOrDefault(q => q.Id.Equals(questionId));
            question.Points += 1;
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
            // questions that has checked with all community
            var all = from q in db.Questions
                      join qc in db.Questions_Comunities on q.Id equals qc.QuestionId
                      where qc.ComunitieId == 16 select new { q };
            var e = result.Concat(all);

            // return the questions not answerd yet
            var AnsweredQuestionsIds = db.User_Answers.Where(a => a.UserId == userId).Select(q => q.QuestionId);
            var r = e.Select(x => x.q.Id).Except(AnsweredQuestionsIds);
            var questions = e.Where(x => r.Contains(x.q.Id));

            foreach (var item in questions.Distinct().OrderByDescending(c => c.q.EndDate))
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
            return ListOFQuestions;

        }

        // return answerd questions for user in its profile
        public List<PostPoco> GetAnsweredQuestions(string userId)
        {
            Question obj;
            List<PostPoco> ListOFQuestions = new List<PostPoco>();
            var result = from q in db.Questions
                         join ua in db.User_Answers on q.Id equals ua.QuestionId
                         where ua.UserId.Equals(userId)
                         
                         select new 
                         {
                            q,  ids=ua.SelectedAnswerIds
                         };
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
                obj.TotalAnswers = item.q.TotalAnswers;

                ListOFQuestions.Add(new PostPoco(obj, item.ids));
            }
            return ListOFQuestions;
        }



        // return All answerd questions for admin in blog index to write the summmery
        public List<PostPoco> GetAnsweredQuestions()
        {
            Question obj;
            List<PostPoco> ListOFQuestions = new List<PostPoco>();
            var result = from q in db.Questions
                         join ua in db.User_Answers on q.Id equals ua.QuestionId
                         
                         select new
                         {
                             q,
                             ids = ua.SelectedAnswerIds
                         };
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
                obj.TotalAnswers = item.q.TotalAnswers;

                ListOFQuestions.Add(new PostPoco(obj, item.ids));
            }
            return ListOFQuestions;
        }

        public List<string> ImageUpload(List<HttpPostedFileBase> Imgs)
        {
            List<string> fileNames = new List<string>();
            if (Imgs != null)
            {
                foreach (var img in Imgs)
                {
                    string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(img.FileName);
                    string Extention = Path.GetExtension(img.FileName);
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileNameWithoutExtention + Extention;
                    //questionImg.SaveAs(HttpContext.Current.Server.MapPath("/Api/UploadedImages/" + fileName));
                    img.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("/Api/UploadedImages/" + fileName));
                    fileNames.Add(fileName);
                }
                return fileNames;
            }
            return fileNames;
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
            Question.PhotoUrl = ImageUpload(viewModel.QPhotoFile).FirstOrDefault();
            Question.AnswerTypeId = db.Answer_Types.Where(t => t.Type == viewModel.AnswerType).FirstOrDefault().Id;
            Question.QuesetionTitle = viewModel.QuesetionTitle;
            Question.IsRequired = viewModel.IsRequired;
            Question.EndDate = viewModel.EndDate;
            Question.IsRankeditorChoice = viewModel.IsRankeditorChoice;



            //fill question answers
            string[] aValues = viewModel.answerValues.Split(new Char[] { ';' });
            var answerImgs = viewModel.answerImgs;
            List<string> optionsImgs = ImageUpload(answerImgs);
            for (int a = 0; a < aValues.Length; a++)
            {
                var answerValues = new Question_Answer_Values();
                answerValues.OptionNum = a;
                answerValues.AnswerValue = aValues[a];
                if (optionsImgs.Count != 0 && optionsImgs.Count >= a)
                {
                    answerValues.PhotoUrl = optionsImgs[a];
                }
              
                //fill question answers to question
                Question.Question_Answer_Values.Add(answerValues);
            }

            //fill question community
            var qCommunity = new Questions_Comunities();
            if (!viewModel.ComunitieIds.Equals(16))
            {
                string[] communiyIds = viewModel.ComunitieIds.Split(new Char[] { ';' });
                for (int c = 0; c < communiyIds.Length; c++)
                {
                    
                    qCommunity.ComunitieId = int.Parse(communiyIds[c]);
                    qCommunity.AddingDate = DateTime.Now;
                    //fill question community into question
                }
            }
            else
            {
                qCommunity.ComunitieId = 16;
                qCommunity.AddingDate = DateTime.Now;
                //fill question community into question
            }
            Question.Questions_Comunities.Add(qCommunity);




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

        
        public bool Update(PostVM viewModel)
        {
            var Question = db.Questions.SingleOrDefault(q => q.Id == viewModel.questionId && q.PostId == viewModel.PostId);

            Question.QuesetionTitle = viewModel.QuesetionTitle;
            Question.PhotoUrl = ImageUpload(viewModel.QPhotoFile).FirstOrDefault();
            Question.IsRequired = viewModel.IsRequired;
            Question.IsRankeditorChoice = viewModel.IsRankeditorChoice;
            Question.EndDate = viewModel.EndDate;
            Question.AnswerTypeId = db.Answer_Types.Where(x => x.Type.Equals(viewModel.AnswerType)).Select(a => a.Id).FirstOrDefault();
            // comunity
            string[] communiyIds = viewModel.ComunitieIds.Split(new Char[] { ';' });

            var qCommunity_old = db.Questions_Comunities.Where(x => x.QuestionId == viewModel.questionId);
            db.Questions_Comunities.RemoveRange(qCommunity_old);

            for (int c = 0; c < communiyIds.Length; c++)
            {
                var qCommunity = new Questions_Comunities();
                qCommunity.QuestionId = viewModel.questionId;
                qCommunity.ComunitieId = int.Parse(communiyIds[c]);
                qCommunity.AddingDate = DateTime.Now;
                //fill question community into question
                Question.Questions_Comunities.Add(qCommunity);
            }

            //var comunityList = Question.Questions_Comunities.ToList();

            //if (communiyIds.Length > comunityList.Count)
            //{
            //    for (int i = 0; i < comunityList.Count; i++)
            //    {
            //        comunityList[i].ComunitieId = int.Parse(communiyIds[i]);
            //    }

            //    for (int i = comunityList.Count; i < communiyIds.Length; i++)
            //        comunityList.Add(new Questions_Comunities()
            //        { QuestionId = viewModel.questionId,
            //            ComunitieId =int.Parse(communiyIds[i]),
            //            AddingDate =DateTime.Now
            //        });
            //}
            //else
            //{

            //    for (int i = 0; i < communiyIds.Length; i++)
            //    {
            //        comunityList[i].ComunitieId = int.Parse(communiyIds[i]);
            //    }
            //    var deletedIDs = Question.Questions_Comunities.OrderByDescending(p => p.Id).Take(comunityList.Count - communiyIds.Length);
            //    foreach (var item in deletedIDs)
            //    {
            //        Question.Questions_Comunities.Remove(item);
            //    }

            //}

            //Question.Questions_Comunities = comunityList;

            //fill question answers
            string[] aValues = viewModel.answerValues.Split(new Char[] { ';' });
            List<string> answerImgs = ImageUpload(viewModel.answerImgs);
            var oldOptions = Question.Question_Answer_Values.ToList();
            for (int a = 0; a < aValues.Length; a++)
            {
                oldOptions[a].AnswerValue = aValues[a];
                if (answerImgs[a] != null)
                {
                    oldOptions[a].PhotoUrl = answerImgs[a];
                }
                else
                {
                    oldOptions[a].PhotoUrl = null;
                }

                //fill question answers to question
                Question.Question_Answer_Values = oldOptions;
            }

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

            //increse NumOfSelectedAnswers by one
            string[] optionIds = VMobj.answerValues.Split(new Char[] { ';' });
            int[] myIDs = Array.ConvertAll(optionIds, s => int.Parse(s));
            var q_answer = db.Question_Answer_Values.Where(a => (myIDs).Contains(a.Id) && a.QuestionId == VMobj.questionId).ToList();
            q_answer.ForEach(a => a.NumOfSelectedAnswers++);
            //for (int i = 0; i < optionIds.Length; i++)
            //{
            //    var q_answer = db.Question_Answer_Values.Single(x => x.QuestionId == VMobj.questionId && x.Id ==int.Parse(optionIds[i]));
            //    if (q_answer.NumOfSelectedAnswers != null)
            //    {
            //        q_answer.NumOfSelectedAnswers += 1;
            //    }
            //    else
            //    {
            //        q_answer.NumOfSelectedAnswers = 1;
            //    }
            //}

            var question = db.Questions.Single(x => x.Id == VMobj.questionId);
            question.TotalAnswers += 1;
            var user = db.AspNetUsers.Find(VMobj.UserId);
            user.Points += 1;
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
