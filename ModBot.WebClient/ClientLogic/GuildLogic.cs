using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
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
using ModBot.WebClient.Extention;

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

        public async Task<string> DiscordGetToken()
        {
            var authenticateResult = await _context.AuthenticateAsync("Discord");
            Token = (authenticateResult.Properties ?? throw new UnauthorizedAccessException()).GetTokenValue("access_token");

            return Token;
        }

        private DiscordRestClient _discordRestClient = new DiscordRestClient();
        public async Task<IList<GuildModel>> GetUserServerAsync(string token)
        {

            await _discordRestClient.LoginAsync(TokenType.Bearer, token);
            

            var guildSummeries = _discordRestClient.GetGuildSummariesAsync();

            var servers = new List<GuildModel>();

            await foreach (var guildsummery in guildSummeries)
            {
                foreach (var guild in guildsummery.Where(g => g.Permissions.Administrator))
                {
                    servers.Add(new GuildModel(guild.Id, guild.Name, guild.IconUrl, controller.hasbot(guild.Id)));
                }
            }

            return servers.OrderByDescending(t => t.HasBot).ToList();

        }
    }
}
