﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
   
    [ApiController]
    public class MemberController : ControllerBase
    {
        public MemberController()
        {

        }
        public async Task<IActionResult> GetMember(ulong id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("is is null");
                }

                
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(500, "internal serve error");
            }
        }
        public async Task<IActionResult> GetAllMembers(ulong id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal serve error");
            }
        }
    }
}
