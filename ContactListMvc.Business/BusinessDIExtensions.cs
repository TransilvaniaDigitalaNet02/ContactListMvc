using ContactListMvc.Business.Abstractions.Services;
using ContactListMvc.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContactListMvc.Business
{
    public static class BusinessDIExtensions
    {
        public static IServiceCollection AddBusinessLogic(
           this IServiceCollection services)
        {
            services.AddTransient<IContactListService, ContactListService>();

            return services;
        }
    }
}
