using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IChangeLogIRepository _changeLogRepo;
        public ChangeLogController(IChangelogService changelogService, IChangeLogIRepository logRepo)
        {
            this._changelogService = changelogService;
            this._changeLogRepo = logRepo;

        }

        [HttpGet]
        [Route(Routes.ChangeLog.GetLog)]
        public async Task<IActionResult> GetChangeLog(int id)
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

        [HttpGet]
        [Route(Routes.ChangeLog.GetAllLogs)]
        public async Task<IActionResult> GetAllLogs()
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
        [Route(Routes.ChangeLog.CreateLog)]
        public async Task<IActionResult> CreateLog()
        {
            try
            {
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpDelete]
        [Route(Routes.ChangeLog.DeleteLog)]
        public async Task<IActionResult> DeleteLog()
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
