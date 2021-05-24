using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO.BannedWordDto;
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
        
        public BannedWordsController(IBannedWordService bannedWordService)
        {
            this._bannedWordService = bannedWordService;
         

        }

        [HttpGet]
        [Route(Routes.BannedWords.GetBannedWord)]
        public async Task<IActionResult> GetBannedWord(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("id is null");
                }

                var bannedWord = await _bannedWordService.GetBannedWord(id);

                if(bannedWord == null)
                {
                    return NotFound("banned word not found");
                }
                
                return Ok(bannedWord);
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
                var bannedWords = await _bannedWordService.GetAllBannedWords();

                if (bannedWords == null)
                {
                    return NotFound(" Banned words is empty");
                }

                return Ok(bannedWords);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPost]
        [Route(Routes.BannedWords.CreateBannedWord)]
        public async Task<IActionResult> CreateBannedWord(CreateBannedWordDto createBannedWord)
        {
            try
            {
               // _bannedWordService.CreateBannedWord(createBannedWord);
                if(createBannedWord == null)
                {
                    return NoContent();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpDelete]
        [Route(Routes.BannedWords.DeleteBannedWord)]
        public async Task<IActionResult> DeleteBannedWord(int id)
        {
            try
            {
                var bannedWord = await _bannedWordService.GetBannedWord(id);
                if(bannedWord == null)
                {
                    return NotFound("Banned wortd not found");
                }

                _bannedWordService.DeleteBannedWord(bannedWord);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPut]
        [Route(Routes.BannedWords.UpdateBannedWord)]
        public async Task<IActionResult> UpdateBannedWord(UpdateBannedWordDto updateBannedWord,int id)
        {
            try
            {
                if(updateBannedWord == null)
                {
                    return BadRequest("object is null");
                }

                var bannedWord = await _bannedWordService.GetBannedWord(id);

                if(bannedWord == null)
                {
                    return NotFound("Banned word not found");
                }

                _bannedWordService.UpdateBannedWord(updateBannedWord, id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
    }
}
