using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rcate_FinancialPortal.Startup))]
namespace rcate_FinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
