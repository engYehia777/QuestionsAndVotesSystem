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
    
    public partial class Post_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post_Info()
        {
            this.Questions = new HashSet<Question>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<int> LanguageId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
