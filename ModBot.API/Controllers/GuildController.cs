using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
   
    [ApiController]
    public class GuildController : ControllerBase
    {
        private readonly IGuildService _guildService;
        private readonly ILoggerManager logger;
        public GuildController(IGuildService guildService, ILoggerManager logger)
        {
            this._guildService = guildService;
            this.logger = logger;
        }


        [HttpPost]
        [Route(Routes.Guilds.GetGuild)]
        public async Task<IActionResult> GetGuild(GuildDto guildDto)
        {
            try
            {
                var guild = await _guildService.GetGuildById(guildDto.Id);

                if(guild == null)
                {
                    logger.Info("guild was null", this.GetType().Name);
                    return NotFound("Guild not found");
                }

                logger.Info($"Guild with id: {guild.Id} sent to client", this.GetType().Name);
                return Ok(guild);            
            }
            catch(Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.Guilds.CreateGuild)]
        public async Task<IActionResult> CreateGuild(GuildDto guildDto)
        {
            try
            {
                Guild guild = new Guild(guildDto.Id, guildDto.HasBot, guildDto.Icon, guildDto.Name);

                bool succeded = await _guildService.CreateGuild(guild);

                logger.Info($"Guild was added to the database: {succeded}", this.GetType().Name);
                return Ok();
            }
            catch(Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.Guilds.UpdateGuild)]
        public async Task<IActionResult> UpdateGuild(GuildDto guildDto)
        {
            try
            {
                Guild guild = new Guild(guildDto.Id, guildDto.HasBot, guildDto.Icon, guildDto.Name);

                bool succeded = await _guildService.UpdateGuild(guild);

                logger.Info($"Guild in database was updated: {succeded}", this.GetType().Name);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet]
        [Route(Routes.Guilds.GetGuilds)]
        public async Task<IActionResult> GetAllGuilds()
        {
            try
            {
                logger.Info("Get all Guilds", this.GetType().Name);
                var guilds = await _guildService.GetAllGuilds();


                logger.Info($"Number of guilds sent to client {guilds.Count()}", this.GetType().Name);
                return Ok(guilds);
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }
    }
}
