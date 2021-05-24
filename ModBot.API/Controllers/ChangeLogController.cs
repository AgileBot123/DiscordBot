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
     
        public ChangeLogController(IChangelogService changelogService)
        {
            this._changelogService = changelogService;
          

        }

        [HttpGet]
        [Route(Routes.ChangeLog.GetLog)]
        public async Task<IActionResult> GetChangeLog(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("id is null");

                var getLog = await _changelogService.GetChangeLog(id);

                if (getLog == null)
                    return NotFound("No Log found ");

                return Ok(getLog);
            }

            catch (Exception)
            {
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
                    return NotFound("Log is empty");
                }

                return Ok(allLogs);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.ChangeLog.CreateLog)]
        public async Task<IActionResult> CreateLog(CreateChangeLogDto createChangeLog)
        {
            try
            {
                if(createChangeLog == null)
                {
                    return NoContent();
                }
                await _changelogService.CreateChangelog(createChangeLog);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpDelete]
        [Route(Routes.ChangeLog.DeleteLog)]
        public async Task<IActionResult> DeleteLog(int id)
        {
            try
            {
                var log = await _changelogService.GetChangeLog(id);
                if(log == null)
                {
                    return NotFound("No log found");
                }

                await _changelogService.DeleteChangelog(log);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
        public async Task<IActionResult> UpdateLog(UpdateChangelogDto updateChangelog, int id)
        {
            try
            {
                if (updateChangelog == null)
                {
                    return BadRequest("object is null");
                }

                var log = await _changelogService.GetChangeLog(id);

                if (log == null)
                {
                    return NotFound();
                }

                await _changelogService.UpdateChangelog(updateChangelog, id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
 
    }
}
