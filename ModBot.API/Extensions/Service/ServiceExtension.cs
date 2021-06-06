using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModBot.Business.Services;
using ModBot.DAL.Data;
using ModBot.DAL.Repository;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Extensions.Service
{
    public static class ServiceExtension
    {

        public static void DependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<DatabaseRepository>();

            services.AddScoped<IPunishmentsLevelsService, PunishmentsLevelsService>();
            services.AddScoped<IBannedWordService, BannedWordService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IGuildService, GuildService>();
            services.AddScoped<ILoggerManager, LoggerManager>();

            services.AddScoped<IBannedWordRepository, DatabaseRepository>();
            services.AddScoped<IMemberRepository, DatabaseRepository>();
            services.AddScoped<IPunishmentsLevelsRepository, DatabaseRepository>();
            services.AddScoped<IGuildRepository, DatabaseRepository>();
        }


        public static void DatabaseConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ModBotContext>(o => o.UseSqlServer(config.GetConnectionString("ModBotDatabase")));
        }

    }
}
