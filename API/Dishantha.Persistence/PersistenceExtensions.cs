using Dishantha.Domain.Repositories;
using Dishantha.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dishantha.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {            
            services
                .AddDbContext<ApplicationDbContext>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))           
                .AddScoped(typeof(ISightingRepository), typeof(SightingRepository));
                            
            return services;
        }
    }
}
