using Microsoft.AspNetCore.Http;
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
            return Ok();
        }
        public async Task<IActionResult> GetAllMembers(ulong id)
        {
            return Ok();
        }
    }
}
