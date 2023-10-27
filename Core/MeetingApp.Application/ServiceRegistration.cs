using AutoMapper;
using MediatR;
using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeetingApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(new List<Profile>
                {
                    new MeetingProfile(),
                    new AppUserProfile(),
                    new ParticipantProfile()
                });
            });
        }
    }
}
