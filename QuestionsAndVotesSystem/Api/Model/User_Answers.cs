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
    
    public partial class User_Answers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public string SelectedAnswerIds { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
