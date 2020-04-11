using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsAndVotesSystem.Api.Model.Poco
{
    public class PostPoco
    {
        public int questionId { get; set; }

        public int postId { get; set; }

        public string answerType { get; set; }

        public string answerTypeName { get; set; }

        public string QuesetionTitle { get; set; }

        public DateTime endDate { get; set; }

        //public List<Question_Answer_Values> answerValues { get; set; }

        public List<AnswerValuesPoco> answerValues { get; set; }


        public string[] userAnswerIds { get; set; }

        public List<string> communityNames { get; set; }

        public List<int> communityIDs { get; set; }

        public Nullable<bool> IsRequired { get; set; }

        public Nullable<bool> IsRankeditorChoice { get; set; }

        public int TotalAnswers { get; set; }

        public string QPhotoFile { get; set; }

        public PostPoco()
        {

        }
        
        public PostPoco(Answer_Types obj, string lang)
        {
            answerType = obj.Type;
            answerTypeName = (lang.Equals("ar-EG")) ?  obj.NameAr : obj.NameEn;
        }

        public PostPoco(Question obj)
        {
            questionId = obj.Id;
            postId = obj.PostId;
            QuesetionTitle = obj.QuesetionTitle;
            QPhotoFile = obj.PhotoUrl;
            IsRequired = obj.IsRequired;
            IsRankeditorChoice = obj.IsRankeditorChoice;
            endDate = obj.EndDate;
            answerType = obj.Answer_Types.Type;
            answerValues = obj.Question_Answer_Values.Select(x => new AnswerValuesPoco
            {
                Id = x.Id,
                QuestionId = x.QuestionId,
                OptionNum = x.OptionNum,
                AnswerValue = x.AnswerValue,
                PhotoUrl = x.PhotoUrl,
                NumOfSelectedAnswers = x.NumOfSelectedAnswers
            }).ToList();
            communityNames = obj.Questions_Comunities.Select(c => c.Community).Select(c => c.NameEn).ToList();
            communityIDs= obj.Questions_Comunities.Select(c => c.ComunitieId).ToList();
        }
        public PostPoco(Question obj, string answerIds)
        {
            questionId = obj.Id;
            postId = obj.PostId;
            QuesetionTitle = obj.QuesetionTitle;
            QPhotoFile = obj.PhotoUrl;
            IsRequired = obj.IsRequired;
            endDate = obj.EndDate;
            TotalAnswers = obj.TotalAnswers;
            answerType = obj.Answer_Types.Type;
            answerValues = obj.Question_Answer_Values.Select(x => new AnswerValuesPoco
            {
                Id = x.Id,
                QuestionId = x.QuestionId,
                OptionNum = x.OptionNum,
                AnswerValue = x.AnswerValue,
                PhotoUrl = x.PhotoUrl,
                NumOfSelectedAnswers = x.NumOfSelectedAnswers
            }).ToList();
            communityNames = obj.Questions_Comunities.Select(c => c.Community).Select(c => c.NameEn).ToList();

            userAnswerIds = answerIds.Split(new Char[] { ';' });
        }

    }
}