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
        private readonly ILoggerManager logger;
        public PunishedLevelsController(IPunishmentsLevelsService punishedLevelService, ILoggerManager logger)
        {
            this._punishedLevelService = punishedLevelService;
            this.logger = logger;
        }


        [HttpGet]
        [Route(Routes.PunishmentLevels.GetPunishmentLevel)]
        public async Task<IActionResult> GetPunishmentLevel(ulong guilId, int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.Info("Id is zero.", this.GetType().Name);
                    return BadRequest("id is null");
                }

                var punishedLevel = await _punishedLevelService.GetPunishmentLevel(guilId, id);

                if (punishedLevel == null)
                {
                    logger.Info($"No punishmentlevel was found in database with id: {id}.", this.GetType().Name);
                    return NotFound("No punishment found ");
                }
                return Ok(punishedLevel);
            }
            catch
            {
                return NotFound("Internal Server Error");
            }
        }


        [HttpPost]
        [Route(Routes.PunishmentLevels.GetPunishmentLevels)]
        public async Task<IActionResult> GetPunishmentLevels(PunishmentSettingsDto punishmentSettingsDto)
        {
            try
            {
                var guildId = punishmentSettingsDto.GuildId;
                var punishmentLevels = await _punishedLevelService.GetPunishmentLevels(guildId);

                if (punishmentLevels == null)
                {
                    logger.Info("No punishmentLevels was found in datbase", this.GetType().Name);
                    return NotFound("Punishments are empty");
                }

                logger.Info($"punishmentlevels was sent to client", this.GetType().Name);
                return Ok(punishmentLevels);
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.PunishmentLevels.CreatePunishmentLevel)]
        public IActionResult CreatePunishmentLevel(PunishmentSettingsDto createPunishment)
        {
            try
            {              
                if (createPunishment == null)
                {
                    logger.Info("PunishmentDto was empty or null", this.GetType().Name);
                    return BadRequest("Parameters is null");
                }
               
                var result =_punishedLevelService.CreatePunishmentLevel(createPunishment);

                if (result)
                {
                    logger.Info("PunishmentLevel was created and sent Statuscode: 204 to client", this.GetType().Name);
                    return NoContent();
                }

                logger.Info("No punishmentlevel was created", this.GetType().Name);
                return BadRequest("PunishedLevel was not created");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete]
        [Route(Routes.PunishmentLevels.DeletePunishmentLevel)]
        public async Task<IActionResult> DeletePunishmentLevel(PunishmentSettingsDto punishment)
        {
            try
            {
                if (punishment.Id == 0)
                {
                    logger.Info("Id in parameter is zero", this.GetType().Name);
                    return BadRequest("Id cannot be 0");
                }
                
               var result = await _punishedLevelService.DeletePunishemntLevel(punishment);

                if (result)
                {
                    logger.Info("PunishmentLevel was deleted and sent Statuscode: 204 to client", this.GetType().Name);
                    return NoContent();
                }

                logger.Info("Punishementlevel was not deleted", this.GetType().Name);
                return BadRequest("PunishedLevel was not created");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.PunishmentLevels.UpdatePunishmentLevel)]
        public async Task<IActionResult> UpdatePunishmentLevel(int id, PunishmentSettingsDto updatePunishment)
        {
            try
            {
                if(updatePunishment == null)
                {
                    logger.Info("PunishmentDto is null", this.GetType().Name);
                    return BadRequest("object is null");
                }
                 
               var result = await _punishedLevelService.UpdatePunishmentLevel(updatePunishment,id);

                if (result)
                {
                    logger.Info("PunishmentLevel was updated and statuscode 204 was sent to Client", this.GetType().Name);
                   return NoContent();
                }

                logger.Info("Punishmentelevel was not updated", this.GetType().Name);
                return BadRequest("Punishmentlevels was not updated");    
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }
    }
}
