﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetBannedWord(string word)
        {
            try
            {
                if(string.IsNullOrEmpty(word))
                {
                    return BadRequest("Word is nullor empty");
                }

                var bannedWord = await _bannedWordService.GetBannedWord(word);

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

                if (bannedWords.Count() == 0)
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
        public IActionResult CreateBannedWord(CreateBannedWordDto createBannedWord)
        {
            try
            {              
                if(createBannedWord == null)               
                    return BadRequest("Parameter cannot be null");
                
               var result =  _bannedWordService.CreateBannedWord(createBannedWord);

                if(result)
                return NoContent();

                return BadRequest("Banned word was not created");
            }
            catch (Exception)
            {
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
                    return BadRequest("id cannot be empty");

              var result = await  _bannedWordService.DeleteBannedWord(word);

                if (result)
                    return NoContent();

                return BadRequest("No banned word found");
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
                if(updateBannedWord == null || id == 0)
                {
                    return BadRequest("Parameters cannot be null and/or id cannot be zero");
                }

               var result = await _bannedWordService.UpdateBannedWord(updateBannedWord, id);

                if (result)
                    return NoContent();

                return BadRequest("banned word was not update");
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");

            }
        }
    }
}
