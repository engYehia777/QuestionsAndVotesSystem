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
    
    public partial class SiteManagement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiteManagement()
        {
            this.SiteManagementSections = new HashSet<SiteManagementSection>();
        }
    
        public int ID { get; set; }
        public string PageNameAR { get; set; }
        public string PageNameEN { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public string URLTitleAR { get; set; }
        public string URLTitleEN { get; set; }
        public string SubTitleAR { get; set; }
        public string SubTitleEN { get; set; }
        public string DetailsAR { get; set; }
        public string DetailsEN { get; set; }
        public string Photo { get; set; }
        public string PhotoCover { get; set; }
        public string PhotoAltAR { get; set; }
        public string PhotoAltEN { get; set; }
        public string BrowserTitleAR { get; set; }
        public string BrowserTitleEN { get; set; }
        public string Routing { get; set; }
        public string MetaDescriptionAR { get; set; }
        public string MetaDescriptionEN { get; set; }
        public string MetaKeywordsAR { get; set; }
        public string MetaKeywordsEN { get; set; }
        public Nullable<int> ViewsAR { get; set; }
        public Nullable<int> ViewsEN { get; set; }
        public Nullable<int> SortAR { get; set; }
        public Nullable<int> SortEN { get; set; }
        public string Note { get; set; }
        public Nullable<int> MenuID { get; set; }
        public Nullable<int> SiteManagementTypeID { get; set; }
        public Nullable<int> UserID_Edit { get; set; }
        public Nullable<System.DateTime> Date_Edit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteManagementSection> SiteManagementSections { get; set; }
    }
}
