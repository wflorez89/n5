using Microsoft.Extensions.DependencyInjection;
using WilmerFlorez.Queries.Interfaces;

namespace WilmerFlorez.Queries.Implementation
{
    public static class ApplicationExtension
    {
        public static void UseQueryApplication(this IServiceCollection services)
        {
            services.AddScoped<IPermissionQueryService, PermissionQueryService>();
        }
    }
}
