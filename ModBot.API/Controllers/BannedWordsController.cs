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
        private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        public BannedWordsController(IBannedWordService bannedWordService)
        {
            this._bannedWordService = bannedWordService;
         

        }

        [HttpGet]
        [Route(Routes.BannedWords.GetBannedWord)]
        public async Task<IActionResult> GetBannedWord(string word)
        {
            try
            {
                if(string.IsNullOrEmpty(word))
                {
                    _log.Info("Parameter is null");
                    return BadRequest("word is null or empty");
                }

                var bannedWord = await _bannedWordService.GetBannedWord(word);

                if(bannedWord == null)
                {
                    _log.Info("No Banned word was found with that id");
                    return NotFound("banned word not found");
                }
                
                return Ok(bannedWord);
            }
            catch(Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500,"internal server error");

            }    
        }

        [HttpGet]
        [Route(Routes.BannedWords.GetAllBannedWords)]
        public async Task<IActionResult> GetAllBannedWords()
        {
            try
            {
                _log.Info("Trying to get all banned words");
                var result = await _bannedWordService.GetAllBannedWords();

                _log.Info($" {result.Count()} Number of bannedwords was returned");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [Route(Routes.BannedWords.CreateBannedWord)]
        public IActionResult CreateBannedWord(BannedWordDto createBannedWord)
        {
            try
            {             
                if(createBannedWord == null)
                {
                    _log.Info("Parameter is null");
                    return BadRequest("Parameters cannot be null");
                }            
                    
                
               var result =  _bannedWordService.CreateBannedWord(createBannedWord);

                if(result)
                {
                    _log.Info($"{createBannedWord} was succesfully created and returns NoContent");
                    return NoContent();
                }


                _log.Info($"No bannedWord was created");
                return BadRequest("Banned word was not created");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete]
        [Route(Routes.BannedWords.DeleteBannedWord)]
        public async Task<IActionResult> DeleteBannedWord(string word)
        {
            try
            {
                if (string.IsNullOrEmpty(word))
                {
                    _log.Info($"Parameter was null");
                    return BadRequest("word cannot be empty or null");
                }

     
                var result = await  _bannedWordService.DeleteBannedWord(word);

                if (result)
                {
                    _log.Info($"Word: {word} was deleted from database");
                    return NoContent();
                }

                _log.Info($"No banned word was deleted, _bannedWordService.DeleteBannedWord returned false");
                return BadRequest("Banned word was not deleted");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, "internal server error");

            }
        }

        [HttpPut]
        [Route(Routes.BannedWords.UpdateBannedWord)]
        public async Task<IActionResult> UpdateBannedWordList(BannedWordListDto updateBannedWordListDto)
        {
            try
            {
                if(updateBannedWordListDto.BannedWordList == null)
                {
                    _log.Info("Parameter was null");
                    return BadRequest("Parameters cannot be null and/or id cannot be zero");
                }

                _log.Info($"Trying to Updated {updateBannedWordListDto.BannedWordList}");
                var result = await _bannedWordService.UpdateBannedWordList(updateBannedWordListDto);

                if (result)
                    return NoContent();


                _log.Info("Result return false, no word was updated");
                return BadRequest("banned word was not update");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500, "internal server error");
            }
        }
    }
}
