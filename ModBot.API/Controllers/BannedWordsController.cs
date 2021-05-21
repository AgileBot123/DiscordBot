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
    public class BannedWordsController : ControllerBase
    {
        public readonly IBannedWordService _bannedWordService;
        public BannedWordsController(IBannedWordService bannedWordService)
        {
            this._bannedWordService = bannedWordService;

        }

        [HttpGet]
        public async Task<IActionResult> GetBannedWord()
        {
            try
            {
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(500,"internal server error");

            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBannedWords()
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
        public async Task<IActionResult> CreateBannedWord()
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
        public async Task<IActionResult> DeleteBannedWord()
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

        [HttpPut]
        public async Task<IActionResult> UpdateBannedWord()
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
