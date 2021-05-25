using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModBot.Business.Services;
using ModBot.DAL.Data;
using ModBot.DAL.Repository;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ModBotContext>(o => o.UseSqlServer(Configuration.GetConnectionString("ModBotDatabase")));

            services.AddScoped<DatabaseRepository>();

            services.AddScoped<IPunishmentsLevelsService, PunishmentsLevelsService>();
            services.AddScoped<IChangelogService, ChangelogService>();
            services.AddScoped<IBannedWordService, BannedWordService>();
            services.AddScoped<IMemberService, MemberService>();

            services.AddScoped<IBannedWordRepository, DatabaseRepository>();
            services.AddScoped<IChangeLogRepository, DatabaseRepository>();
            services.AddScoped<IMemberRepository, DatabaseRepository>();
            services.AddScoped<IPunishmentsLevelsRepository, DatabaseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
