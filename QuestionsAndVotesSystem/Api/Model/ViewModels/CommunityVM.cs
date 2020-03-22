using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionsAndVotesSystem.Api.Model.Poco;

namespace QuestionsAndVotesSystem.Api.Model.ViewModels
{
    public class CommunityVM
    {
        public List<CommunityPoco> communities { get; set; }

        public CommunityPoco Community { get; set; }

        public string userId { get; set; }
    }
}