using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENoticeBoard.Startup))]
namespace ENoticeBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
