using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.ServiceInterface;
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
        public GuildController(IGuildService guildService)
        {
            this._guildService = guildService;
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
                    return NotFound("Guild not found");
                }
                return Ok(guild);            
            }
            catch(Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet]
        [Route(Routes.Guilds.GetGuilds)]
        public async Task<IActionResult> GetAllGuilds()
        {
            try
            {
                var guilds = await _guildService.GetAllGuilds();

                return Ok(guilds);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }
    }
}
