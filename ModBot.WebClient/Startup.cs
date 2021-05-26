using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using AspNet.Security.OAuth.Discord;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Globalization;
using System.Security.Claims;
using static AspNet.Security.OAuth.Discord.DiscordAuthenticationConstants;

namespace ModBot.WebClient
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddAuthorization();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                   .AddCookie(o => o.LoginPath = new PathString("/Account/Login"))
                   .AddDiscord(options =>
                    {

                        options.ClientId = "844535789882834955";
                        options.ClientSecret = "8aAbAg5Ehox_npB16UU2EyEJE5tGs6HS";
                        //options.UserInformationEndpoint = "https://discord.com/api/users/@me/guilds"; might want to remove this later on
                        options.Scope.Add("guilds");
                        options.Scope.Add("guilds.join");

                        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
                        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                        options.ClaimActions.MapJsonKey(Claims.AvatarHash, "avatar");
                        options.ClaimActions.MapJsonKey(Claims.Discriminator, "discriminator");

                        options.SaveTokens = true; // ???? wat this, how long save yes?
                        options.Events = new OAuthEvents
                        {
                            OnCreatingTicket = async context =>
                            {
                                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                                response.EnsureSuccessStatusCode();

                                var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

                                context.RunClaimActions(user);

                                List<AuthenticationToken> tokens = context.Properties.GetTokens().ToList();
                                tokens.Add(new AuthenticationToken()
                                {
                                    Name = "TicketCreated",
                                    Value = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
                                });

                                context.Properties.StoreTokens(tokens);
                            }
                        };
                        services.AddHealthChecks();

                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Start}/{id?}");
            });
        }
    }
}
