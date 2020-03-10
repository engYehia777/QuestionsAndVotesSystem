using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuestionsAndVotesSystem.Startup))]
namespace QuestionsAndVotesSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
