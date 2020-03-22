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

        public List<Question_Answer_Values> answerValues { get; set; }
        public List<string> communityNames { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public System.DateTime EndDate { get; set; }
        public string QPhotoFile { get; set; }
 

        
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
            endDate = obj.EndDate;
            answerType = obj.Answer_Types.Type;
            answerValues = obj.Question_Answer_Values.ToList();
            communityNames= obj.Questions_Comunities.Select(c => c.Community).Select(c => c.NameEn).ToList();

        }
      
    }
}