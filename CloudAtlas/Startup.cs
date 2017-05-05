using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudAtlas.Startup))]
namespace CloudAtlas
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
