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
    
    public partial class Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Language()
        {
            this.MailingLists = new HashSet<MailingList>();
        }
    
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Show { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> UserID_Add { get; set; }
        public Nullable<System.DateTime> Date_Add { get; set; }
        public Nullable<int> UserID_Edit { get; set; }
        public Nullable<System.DateTime> Date_Edit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MailingList> MailingLists { get; set; }
    }
}
