using MeetingApp.Application.Interfaces;
using MeetingApp.Persistence.Repositories;
using MeetingApp.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IMailService), typeof(MailService));
        }
    }
}
