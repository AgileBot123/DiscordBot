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
        public StatisticsController(IStatisticsService statsService)
        {
            _statsService = statsService;
        }

        
        [HttpGet]
        [Route(Routes.Statistisc.GetAllStats)]
        public async Task<IActionResult> GetAllStats()
        {
            try
            {
                return Ok(await _statsService.GetAllStatistics());
            }
            catch (Exception)
            {
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
                    return Ok(stats);
                }

                return NotFound("No Stats with that ID was found");
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.Statistisc.RefreshStats)]
        public async Task<IActionResult> RefreshStats()
        {
            try
            {
                var created = await _statsService.RefreshStatisticsInfo();

                if (created)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }
        
  

    }
}
