using ContactListMvc.Business.Services.Abstractions;
using ContactListMvc.Business.Services.Implementation;
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