using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeLogController : ControllerBase
    {
        private readonly IChangelogService _changelogService;
        public ChangeLogController(IChangelogService changelogService)
        {
            this._changelogService = changelogService;

        }

        [HttpGet]
        public async Task<IActionResult> GetChangeLog()
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
        public async Task<IActionResult> CreateLog()
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
