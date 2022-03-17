using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Options
{
    public static class ConfigureServicesCollection
    {
        public static void AddAllOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleApiOptions>(configuration.GetSection("GoogleApi"));
        }
    }
}
