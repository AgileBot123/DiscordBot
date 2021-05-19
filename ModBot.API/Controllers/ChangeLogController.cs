using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ChangeLogController()
        {

        }
        public IActionResult GetChangeLog()
        {
            return Ok();
        }
        public IActionResult GetAllLogs()
        {
            return Ok();
        }
        public IActionResult CreateLog()
        {
            return Ok();
        }
        public IActionResult DeleteLog()
        {
            return Ok();
        }
 
    }
}
