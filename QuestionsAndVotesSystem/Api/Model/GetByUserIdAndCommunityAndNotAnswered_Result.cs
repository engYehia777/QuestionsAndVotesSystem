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
    
    public partial class GetByUserIdAndCommunityAndNotAnswered_Result
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AnswerTypeId { get; set; }
        public string QuesetionTitle { get; set; }
        public string PhotoUrl { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<bool> IsRankeditorChoice { get; set; }
        public Nullable<int> Points { get; set; }
        public int TotalAnswers { get; set; }
        public string UserId { get; set; }
    }
}
