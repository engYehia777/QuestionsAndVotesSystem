using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionsAndVotesSystem.Api.Model.Poco
{
    public class CommunityPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //public CommunityPoco()
        //{

        //}
        public CommunityPoco(Community obj, string lang )
        {
            Id = obj.Id;
            Name = (lang.Equals("ar-EG")) ? obj.NameAr : obj.NameEn;
            Description = (lang.Equals("ar-EG")) ? obj.DescriptionAr : obj.DescriptionEn;
        }
    }

}