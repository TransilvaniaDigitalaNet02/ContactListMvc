using ContactListMvc.Business.Abstractions.Stores;
using ContactListMvc.Infrastructure.Data;
using ContactListMvc.Infrastructure.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactListMvc.Infrastructure
{
    public static class InfrastructureDIExtensions
    {
        public static IServiceCollection AddInfrastructre(
            this IServiceCollection services,
            string databaseConnectionString)
        {
            if (string.IsNullOrEmpty(databaseConnectionString))
            {
                throw new ArgumentNullException(nameof(databaseConnectionString));
            }

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(databaseConnectionString));

            services.AddTransient<IContactListEntryStore, ContactListEntryStore>();

            return services;
        }
    }
}

