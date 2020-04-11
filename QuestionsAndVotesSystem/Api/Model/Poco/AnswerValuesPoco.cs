using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionsAndVotesSystem.Api.Model.Poco
{
    public class AnswerValuesPoco
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OptionNum { get; set; }
        public string AnswerValue { get; set; }
        public string PhotoUrl { get; set; }
        public int NumOfSelectedAnswers { get; set; }
    }
}