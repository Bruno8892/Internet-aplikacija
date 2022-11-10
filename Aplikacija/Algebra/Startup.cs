using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Algebra.Startup))]
namespace Algebra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
