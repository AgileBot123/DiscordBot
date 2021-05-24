using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Linq;
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
                {
                    return BadRequest("id is null");
                }

                var punishedLevel = await _punishedLevelService.GetPunishmentLevel(id);

                if (punishedLevel == null)
                {
                    return NotFound("No punishment found ");
                }

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

                if (punishmentLevels.Count() == 0)
                {
                    return NotFound("Punishemnts is empty");
                }
                
                return Ok(punishmentLevels);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.PunishedLevels.CreatePunishedLevel)]
        public IActionResult CreatePunishedLevel(CreatePunishmentDto createPunishment)
        {
            try
            {              
                if (createPunishment == null)              
                    return BadRequest("Parameters is null");
               
                var result =_punishedLevelService.CreatePunishmentLevel(createPunishment);

                if (result)
                    return NoContent();
                
                return BadRequest("PunishedLevel was not created");
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
                if (id == 0)                
                    return BadRequest("Id cannot be 0");
                
               var result = await _punishedLevelService.DeletePunishemntLevel(id);

                if (result)              
                    return NoContent();

                return BadRequest("PunishedLevel was not created");
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
                    return BadRequest("object is null");
                
               var result = await _punishedLevelService.UpdatePunishmentLevel(updatePunishment,id);

                if (result)           
                   return NoContent();

                return BadRequest("Punishmentlevels was not updated");    
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }
    }
}
