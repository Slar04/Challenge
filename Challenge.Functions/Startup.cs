using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Service.Implementation;
using Service.Interfaces;
[assembly: FunctionsStartup(typeof(Challenge.Functions.Startup))]

namespace Challenge.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
