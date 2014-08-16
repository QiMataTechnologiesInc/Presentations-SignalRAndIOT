using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Magenic.SignalRanIOT.Server.Startup))]
namespace Magenic.SignalRanIOT.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
