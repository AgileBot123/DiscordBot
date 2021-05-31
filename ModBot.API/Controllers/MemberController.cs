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
        private readonly ILoggerManager logger;
        public MemberController(IMemberService memberService, ILoggerManager logger)
        {
            this._memberService = memberService;
            this.logger = logger;
        }


        [HttpGet]
        [Route(Routes.Members.GetMember)]
        public async Task<IActionResult> GetMember(ulong id)
        {
            try
            {             
                if(id == 0)
                {
                    logger.Info($"{id} was Zero.", this.GetType().Name);
                    return BadRequest("id is is null");
                }

                var member = await _memberService.GetMemberById(id);

                if(member == null)
                {
                    logger.Info($"No members was found with {id} in database.", this.GetType().Name);
                    return NotFound("Member not found");
                }

                logger.Info($"Sent member with id {member.Id} to client", this.GetType().Name);
                return Ok(member);            
            }
            catch(Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
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
                    logger.Info($"Number of members return from database {members.Count()}", this.GetType().Name);
                    return NotFound("Member is empty");
                }

                return Ok(members);
            }
            catch (Exception ex)
            {
                logger.Error(ex, this.GetType().Name);
                return StatusCode(500, "internal server error");
            }
        }
    }
}
