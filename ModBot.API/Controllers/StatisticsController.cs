using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statsService;
        private readonly ILoggerManager logger;
        public StatisticsController(IStatisticsService statsService, ILoggerManager logger)
        {
            _statsService = statsService;
            this.logger = logger;
        }


        [HttpGet]
        [Route(Routes.Statistisc.GetAllStats)]
        public async Task<IActionResult> GetAllStats()
        {
            try
            {
                logger.Info("Getting all the statistics", this.GetType().Name);
                return Ok(await _statsService.GetAllStatistics());
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet]
        [Route(Routes.Statistisc.GetSpecificStats)]
        public async Task<IActionResult> GetStatsById(int id) 
        {
            try
            {
                var stats = await _statsService.GetSpecificStats(id);

                if (stats != null)
                {
                    logger.Info("returning stats to Client", this.GetType().Name);
                    return Ok(stats);
                }

                logger.Info("No stats was found with that id", this.GetType().Name);
                return NotFound("No Stats with that ID was found");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.Statistisc.RefreshStats)]
        public async Task<IActionResult> RefreshStats(ulong guildId)
        {
            try
            {
                var created = await _statsService.RefreshStatisticsInfo(guildId);

                if (created)
                {
                    logger.Info("RefreshStats was refreshed", this.GetType().Name);
                    return NoContent();
                }

                logger.Info("Stats was not refreshed", this.GetType().Name);
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }


    }
}
