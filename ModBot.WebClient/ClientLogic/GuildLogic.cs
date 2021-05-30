using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Rest;
using ModBot.WebClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using ModBot.WebClient.Controllers;
using Newtonsoft.Json;

namespace ModBot.WebClient.ClientLogic
{
    public class GuildLogic

    {
        private string Token;

        private readonly AuthenticationController controller;

        private readonly HttpContext _context;
        public GuildLogic(AuthenticationController controller)
        {
            _context = new HttpContextAccessor().HttpContext;
            this.controller = controller;
        }

        private DiscordRestClient _discordRestClient = new DiscordRestClient();
        public async Task<IList<DiscordServer>> GetUserServerAsync()
        {
            var authenticateResult = await _context.AuthenticateAsync("Discord");
            Token = (authenticateResult.Properties ?? throw new UnauthorizedAccessException()).GetTokenValue("access_token");

            await _discordRestClient.LoginAsync(TokenType.Bearer, Token);
            

            var guildSummeries = _discordRestClient.GetGuildSummariesAsync();

            var servers = new List<DiscordServer>();

            await foreach (var guildsummery in guildSummeries)
            {
                foreach (var guild in guildsummery.Where(g => g.Permissions.Administrator))
                {
                    //var JsonString = controller.hasbot(guild.Id).ToString();
                    //var hasbot = JsonConvert.DeserializeObject<bool>(JsonString);
                    servers.Add(new DiscordServer(guild.Id, guild.Name, guild.IconUrl, controller.hasbot(guild.Id)));
                }
            }

            return servers;

        }
    }
}
