
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
           this IServiceCollection services, IConfiguration config
       )
        {
            services.AddDbContext<BlogNewsContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}