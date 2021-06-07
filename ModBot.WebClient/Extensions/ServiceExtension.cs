using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using static AspNet.Security.OAuth.Discord.DiscordAuthenticationConstants;

namespace ModBot.WebClient.Extensions
{
    public static class ServiceExtension
    {
        public static void AuthenticationConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(o => o.LoginPath = new PathString("/Account/Login"))
            .AddDiscord(options =>
            {
                options.ClientId = Configuration["DiscordClientId:ClientId"];
                options.ClientSecret = Configuration["DiscordClientId:ClientSecret"];
                options.Scope.Add("guilds");
                options.Scope.Add("guilds.join");

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(Claims.AvatarHash, "avatar");
                options.ClaimActions.MapJsonKey(Claims.Discriminator, "discriminator");

                options.SaveTokens = true;
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
        public static void SessionConfig(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = "Guild.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
