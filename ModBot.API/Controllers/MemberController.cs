using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.Extensions.Routes;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
   
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            this._memberService = memberService;
        }


        [HttpGet]
        [Route(Routes.Members.GetMember)]
        public async Task<IActionResult> GetMember(ulong id)
        {
            try
            {
              
                if(id == 0)
                {
                    return BadRequest("is is null");
                }
                var member = await _memberService.GetMemberById(id);

                if(member == null)
                {
                    return NotFound("Member not found");
                }
                return Ok(member);            
            }
            catch(Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet]
        [Route(Routes.Members.GetMembers)]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembers();
                if(members.Count() == 0)
                {
                    return NotFound("Member is empty");
                }

                return Ok(members);
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }
        }
    }
}
