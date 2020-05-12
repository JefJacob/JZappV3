using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JZappV3.Startup))]
namespace JZappV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
