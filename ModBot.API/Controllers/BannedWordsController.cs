using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO.BannedWordDtos;
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
        private readonly ILoggerManager logger;
        public BannedWordsController(IBannedWordService bannedWordService, ILoggerManager logger)
        {
            this._bannedWordService = bannedWordService;
            this.logger = logger;
        }

        [HttpGet]
        [Route(Routes.BannedWords.GetBannedWord)]
        public async Task<IActionResult> GetBannedWord(BannedWordDto bannedWordDto)
        {
            try
            {
                if(string.IsNullOrEmpty(bannedWordDto.Profanity))
                {
                    logger.Info("Parameter is null", this.GetType().Name);
                    return BadRequest("word is null or empty");
                }

                var bannedWord = await _bannedWordService.GetBannedWord(bannedWordDto.GuildId, bannedWordDto.Profanity);

                if(bannedWord == null)
                {
                    logger.Info("No Banned word was found with that id", this.GetType().Name);
                    return NotFound("banned word not found");
                }
                
                return Ok(bannedWord);
            }
            catch(Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500,"internal server error");

            }    
        }

        [HttpPost]
        [Route(Routes.BannedWords.GetAllBannedWords)]
        public async Task<IActionResult> GetAllBannedWords(BannedWordDto bannedWordDto)
        {
            try
            {
                logger.Info("Trying to get all banned words", this.GetType().Name);
                var result = await _bannedWordService.GetAllBannedWords(bannedWordDto.GuildId);

                logger.Info($" {result.Count()} Number of bannedwords was returned", this.GetType().Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
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
                    logger.Info("Parameter is null", this.GetType().Name);
                    return BadRequest("Parameters cannot be null");
                }            
                    
                
               var result =  _bannedWordService.CreateBannedWord(createBannedWord);

                if(result)
                {
                    logger.Info($"{createBannedWord} was succesfully created and returns NoContent", this.GetType().Name);
                    return NoContent();
                }


                logger.Info($"No bannedWord was created", this.GetType().Name);
                return BadRequest("Banned word was not created");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }

        [HttpDelete]
        [Route(Routes.BannedWords.DeleteBannedWord)]
        public async Task<IActionResult> DeleteBannedWord(BannedWordDto bannedWordDto)
        {
            try
            {
                if (string.IsNullOrEmpty(bannedWordDto.Profanity))
                {
                    logger.Info($"Parameter was null", this.GetType().Name);
                    return BadRequest("word cannot be empty or null");
                }

     
                var result = await  _bannedWordService.DeleteBannedWord(bannedWordDto.GuildId, bannedWordDto.Profanity);

                if (result)
                {
                    logger.Info($"Word: {bannedWordDto.Profanity} was deleted from database", this.GetType().Name);
                    return NoContent();
                }

                logger.Info($"No banned word was deleted, _bannedWordService.DeleteBannedWord returned false", this.GetType().Name);
                return BadRequest("Banned word was not deleted");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
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
                    logger.Info("Parameter was null", this.GetType().Name);
                    return BadRequest("Parameters cannot be null and/or id cannot be zero");
                }

                logger.Info($"Trying to Updated {updateBannedWordListDto.BannedWordList}", this.GetType().Name);
                var result = await _bannedWordService.UpdateBannedWordList(updateBannedWordListDto);

                if (result)
                    return NoContent();


                logger.Info("Result return false, no word was updated", this.GetType().Name);
                return BadRequest("banned word was not update");
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }
    }
}
