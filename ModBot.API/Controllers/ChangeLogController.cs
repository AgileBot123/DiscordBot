using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
    [ApiController]
    public class ChangeLogController : ControllerBase
    {
        private readonly IChangelogService _changelogService;
        private readonly ILoggerManager logger;
        public ChangeLogController(IChangelogService changelogService, ILoggerManager logger)
        {
            this._changelogService = changelogService;
            this.logger = logger;
        }

        [HttpGet]
        [Route(Routes.ChangeLog.GetLog)]
        public async Task<IActionResult> GetChangeLog(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.Info("Id is 0", this.GetType().Name);
                    return BadRequest("id is zero");
                }
                                   
                var getLog = await _changelogService.GetChangeLog(id);


                if (getLog == null)
                {
                    logger.Info("No Get log was found in database.", this.GetType().Name);
                    return NotFound("No Log found ");
                }

                logger.Info($"Log with ID: {getLog.Id} was sent to client", this.GetType().Name);
                return Ok(getLog);
            }

            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet]
        [Route(Routes.ChangeLog.GetAllLogs)]
        public async Task<IActionResult> GetAllLogs()
        {
            try
            {
                var allLogs = await _changelogService.GetAllChangelogs();
                if(allLogs.Count() == 0)
                {
                    logger.Info("No logs in database", this.GetType().Name);
                    return NotFound("Log is empty");
                }


                logger.Info($"Sending {allLogs.Count()} to the client.", this.GetType().Name);
                return Ok(allLogs);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.ChangeLog.CreateLog)]
        public IActionResult CreateLog(ChangeLogDto createChangeLog)
        {
            try
            {
                if(createChangeLog == null)
                {
                    logger.Info("CreateChangelogDto was null", this.GetType().Name);
                    return BadRequest("Parameters is null");
                }            
                
                var result = _changelogService.CreateChangelog(createChangeLog);

                if (result)
                {
                    logger.Info("CreateChangelog returns true", this.GetType().Name);
                    return NoContent();
                }

                logger.Info("No Createlog was created", this.GetType().Name);
                return BadRequest("Log was not created");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete]
        [Route(Routes.ChangeLog.DeleteLog)]
        public async Task<IActionResult> DeleteLog(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.Info($"{id} was zero.", this.GetType().Name);
                    return BadRequest("Id cannot be empty");
                }

               var result = await _changelogService.DeleteChangelog(id);

                if (result)
                {
                    logger.Info("Changelog was deleted", this.GetType().Name);
                    return NoContent();
                }

                logger.Info("Change log could not deleted.", this.GetType().Name);
                return BadRequest("log could not delete");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPut]
        [Route(Routes.ChangeLog.UpdateLog)]
        public async Task<IActionResult> UpdateLog(ChangeLogDto updateChangelog, int id)
        {
            try
            {
                if (updateChangelog == null)
                {
                    logger.Info($"Dto is null", this.GetType().Name);
                    return BadRequest("object is null");
                }

                var result = await _changelogService.UpdateChangelog(updateChangelog, id);

                if (result)
                {
                    logger.Info("Changelog was succesfully updated", this.GetType().Name);
                    return NotFound();
                }

                logger.Info("Changelog was not updated, something went wrong in repo", this.GetType().Name);
                return BadRequest("Log could not update");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }
 
    }
}
