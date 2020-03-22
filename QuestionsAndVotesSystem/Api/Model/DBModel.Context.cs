﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer_Types> Answer_Types { get; set; }
        public virtual DbSet<Community> Communities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DeviceType> DeviceTypes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MailingList> MailingLists { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Post_Info> Post_Info { get; set; }
        public virtual DbSet<Question_Answer_Values> Question_Answer_Values { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questions_Comunities> Questions_Comunities { get; set; }
        public virtual DbSet<Roleprimission> Roleprimissions { get; set; }
        public virtual DbSet<SiteManagement> SiteManagements { get; set; }
        public virtual DbSet<SiteManagementSection> SiteManagementSections { get; set; }
        public virtual DbSet<SiteManagementType> SiteManagementTypes { get; set; }
        public virtual DbSet<SocialNetwork> SocialNetworks { get; set; }
        public virtual DbSet<UploadFile> UploadFiles { get; set; }
        public virtual DbSet<User_Answers> User_Answers { get; set; }
        public virtual DbSet<User_Communities> User_Communities { get; set; }
        public virtual DbSet<WebsiteInfo> WebsiteInfoes { get; set; }
        public virtual DbSet<WebsiteInfoEmailSetting> WebsiteInfoEmailSettings { get; set; }
        public virtual DbSet<WebsiteInfoEmailSettingType> WebsiteInfoEmailSettingTypes { get; set; }
        public virtual DbSet<WebsiteInfoSmsAndNotification> WebsiteInfoSmsAndNotifications { get; set; }
        public virtual DbSet<WebsiteInfoSmsAndNotificationStatu> WebsiteInfoSmsAndNotificationStatus { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    
        [DbFunction("DBEntities", "Split")]
        public virtual IQueryable<Split_Result> Split(string delimited, string delimiter)
        {
            var delimitedParameter = delimited != null ?
                new ObjectParameter("delimited", delimited) :
                new ObjectParameter("delimited", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("delimiter", delimiter) :
                new ObjectParameter("delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[DBEntities].[Split](@delimited, @delimiter)", delimitedParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<UserPages_Result> UserPages(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserPages_Result>("UserPages", userIDParameter);
        }
    
        public virtual ObjectResult<UserSecurityPages_Result> UserSecurityPages(string userID, string pageName)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var pageNameParameter = pageName != null ?
                new ObjectParameter("PageName", pageName) :
                new ObjectParameter("PageName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserSecurityPages_Result>("UserSecurityPages", userIDParameter, pageNameParameter);
        }
    }
}
