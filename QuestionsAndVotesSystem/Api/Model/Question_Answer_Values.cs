//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuestionsAndVotesSystem.Api.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question_Answer_Values
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OptionNum { get; set; }
        public string AnswerValue { get; set; }
        public string PhotoUrl { get; set; }
        public Nullable<int> NumOfSelectedAnswers { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
