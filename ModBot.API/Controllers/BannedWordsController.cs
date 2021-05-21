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
    public class BannedWordsController : ControllerBase
    {
        public readonly IBannedWordService _bannedWordService;
        public readonly IBannedWordIRepository _bannedWordRepo;
        public BannedWordsController(IBannedWordService bannedWordService,IBannedWordIRepository bannedWord)
        {
            this._bannedWordService = bannedWordService;
            this._bannedWordRepo = bannedWord;

        }

        [HttpGet]
        [Route(Routes.BannedWords.GetBannedWord)]
        public async Task<IActionResult> GetBannedWord(int id)
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
        [Route(Routes.BannedWords.GetAllBannedWords)]
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
        [Route(Routes.BannedWords.CreateBannedWord)]
        public async Task<IActionResult> CreateBannedWord()
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
        [Route(Routes.BannedWords.DeleteBannedWord)]
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
        [Route(Routes.BannedWords.UpdateBannedWord)]
        public async Task<IActionResult> UpdateBannedWord()
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
    }
}
