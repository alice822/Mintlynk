using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MintLynk.Web.Areas.Identity.IdentityHostingStartup))]
namespace MintLynk.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}