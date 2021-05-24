using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
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

                var punishedLevel = await _punishedLevelService.GetPunishmentLevel(id);

                if (punishedLevel == null)
                    return NotFound("No punishment ");

                return Ok(punishedLevel);
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
                var punishmentLevels = await _punishedLevelService.GetAllPunishmentLevels();

                if (punishmentLevels == null)
                    return NotFound("No Punishemnts");

                return Ok(punishmentLevels);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.PunishedLevels.CreatePunishedLevel)]
        public async Task<IActionResult> CreatePunishedLevel(CreatePunishmentDto createPunishment)
        {
            try
            {
                 _punishedLevelService.CreatePunishmentLevel(createPunishment);

                if (createPunishment == null)
                {
                    return NoContent();
                }

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
                var punishment = await _punishedLevelService.GetPunishmentLevel(id);

                if (punishment == null)
                {
                    return NotFound("No punishemnt found");
                }

                _punishedLevelService.DeletePunishemntLevel(punishment);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPut]
        [Route(Routes.PunishedLevels.DeletePunishedLevel)]
        public async Task<IActionResult> UpdatePunishedLevel(int id, UpdatePunishmentLevelDto updatePunishment)
        {
            try
            {
                if(updatePunishment == null)
                {
                    return BadRequest("object is null");
                }

                var punishment = await _punishedLevelService.GetPunishmentLevel(id);

                if(punishment == null)
                {
                    return NotFound();
                }

                _punishedLevelService.UpdatePunishmentLevel(updatePunishment,id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
    }
}
