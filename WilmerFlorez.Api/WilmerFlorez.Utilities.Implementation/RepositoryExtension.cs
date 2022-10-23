using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WilmerFlorez.Utilities.Implementation.Kafka;
using WilmerFlorez.Utilities.Implementation.Repositories;
using WilmerFlorez.Utilities.Implementation.UnitOfWorks;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repositories;
using WilmerFlorez.Utilities.Interfaces.UnitOfWorks;

namespace WilmerFlorez.Utilities.Implementation
{
    public static class RepositoryExtension
    {
        public static void UseRepository(this IServiceCollection services, Type dbContextType)
        {
            services.AddScoped(typeof(DbContext), dbContextType);
            services.AddScoped(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEventProducer, AccountEventProducer>();
        }
    }
}
