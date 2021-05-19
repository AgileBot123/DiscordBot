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
    public class BannedWordsController : ControllerBase
    {
        public BannedWordsController()
        {

        }

        public IActionResult GetBannedWord()
        {
            return Ok();
        }
        public IActionResult GetAllBannedWords()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBannedWord()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBannedWord()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBannedWord()
        {
            return Ok();
        }
    }
}
