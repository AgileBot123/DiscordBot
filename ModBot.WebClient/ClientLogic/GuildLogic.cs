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

namespace ModBot.WebClient.ClientLogic
{
    public class GuildLogic
    {
        private string Token;

        private readonly HttpContext _context;
        public GuildLogic()
        {
            _context = new HttpContextAccessor().HttpContext;
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
                    servers.Add(new DiscordServer(guild.Id, guild.Name, guild.IconUrl) );
                }
            }

            return servers;

        }
    }
}
