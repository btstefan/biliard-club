using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bilijar.Startup))]
namespace Bilijar
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
