using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyBanHang.Startup))]
namespace QuanLyBanHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
