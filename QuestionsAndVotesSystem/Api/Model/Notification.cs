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
    
    public partial class Notification
    {
        public int NotificationID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Title { get; set; }
        public string TitleEN { get; set; }
        public string Details { get; set; }
        public string DetailsEN { get; set; }
        public string Photo { get; set; }
        public Nullable<int> NotificationTypeID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemTitle { get; set; }
        public Nullable<bool> IsSend { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public Nullable<long> Views { get; set; }
        public Nullable<int> UserID_Add { get; set; }
        public Nullable<System.DateTime> Date_Add { get; set; }
        public Nullable<int> UserID_Edit { get; set; }
        public Nullable<System.DateTime> Date_Edit { get; set; }
    }
}
