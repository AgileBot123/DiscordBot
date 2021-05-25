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
        public IActionResult CreateLog(ChangeLogDto createChangeLog)
        {
            try
            {
                if(createChangeLog == null)               
                    return BadRequest("Parameters is null");
                
                var result = _changelogService.CreateChangelog(createChangeLog);

                if (result)
                    return NoContent();

                return BadRequest("Log was not created");
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
                if (id == 0)
                    return BadRequest("Id cannot be empty");

               var result = await _changelogService.DeleteChangelog(id);

                if(result)
                return NoContent();

                return BadRequest("log could not delete");
            }
            catch (Exception)
            {
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
                    return BadRequest("object is null");
                }

                var result = await _changelogService.UpdateChangelog(updateChangelog, id);

                if (result)
                {
                    return NotFound();
                }

                return BadRequest("Log could not update");
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
 
    }
}
