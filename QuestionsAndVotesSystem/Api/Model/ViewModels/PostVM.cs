using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionsAndVotesSystem.Api.Model;
using QuestionsAndVotesSystem.Api.Model.Poco;
using System.ComponentModel.DataAnnotations;

namespace QuestionsAndVotesSystem.Api.Model.ViewModels
{
    public class PostVM
    {
        // list of comunity
        public SelectList communities { get; set; }
        // list of answer types
        public SelectList answerTypes { get; set; }

        public List<PostPoco> indexQuestions { get; set; }


        public int questionId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public System.DateTime PostEndDate { get; set; }
        public string ComunitieIds { get; set; }
        public int AnswerTypeId { get; set; }
        public string AnswerType { get; set; }
        
        public string QuesetionTitle { get; set; }
        public HttpPostedFileBase QPhotoFile { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<bool> IsRankeditorChoice { get; set; }
        public int LanguageId { get; set; }
        public int OptionNum { get; set; }
        public string answerValues { get; set; }
        public List<HttpPostedFileBase> answerImgs { get; set; }

        

    }
}