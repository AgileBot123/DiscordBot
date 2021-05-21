using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
    
    [ApiController]
    public class PunishedLevelsController : ControllerBase
    {
        private readonly IPunishmentsLevelsService _punishedLevelService;
        public PunishedLevelsController(IPunishmentsLevelsService punishedLevelService)
        {
            this._punishedLevelService = punishedLevelService;
        }

        [HttpGet]
        [Route(Routes.PunishedLevels.GetPunishedLevel)]
        public async Task<IActionResult> GetPunishedLevel(int id)
        {
             try
            {
                if (id == 0)
                    return BadRequest("id is null");

                var punishedLevel = await _punishedLevelService.GetPunishedLevel(id);

                if (punishedLevel == null)
                    return NotFound("No punished ");

                return Ok();
            }

            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpGet]
        [Route(Routes.PunishedLevels.GetPunishedLevels)]
        public async Task<IActionResult> GetPunishedLevels()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.PunishedLevels.CreatePunishedLevel)]
        public async Task<IActionResult> CreatePunishedLevel()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpDelete]
        [Route(Routes.PunishedLevels.DeletePunishedLevel)]
        public async Task<IActionResult> DeletePunishedLevel(int id)
        {
             try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPut]
        [Route(Routes.PunishedLevels.DeletePunishedLevel)]
        public async Task<IActionResult> UpdatePunishedLevel(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
    }
}
