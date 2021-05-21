using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace ModBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunishedLevelsController : ControllerBase
    {
        private readonly IPunishedLevelService _punishedLevelService;
        public PunishedLevelsController(IPunishedLevelService punishedLevelService)
        {
            this._punishedLevelService = punishedLevelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPunishedLevel()
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

        [HttpGet]
        public async Task<IActionResult> GetPunishedLevels(int id)
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
        public async Task<IActionResult> CreatePunishedLevel()
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
        public async Task<IActionResult> DeletePunishedLevel(int id)
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
        public async Task<IActionResult> UpdatePunishedLevel(int id)
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
