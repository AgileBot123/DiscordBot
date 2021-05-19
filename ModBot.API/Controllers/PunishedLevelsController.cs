using Microsoft.AspNetCore.Mvc;

namespace ModBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunishedLevelsController : ControllerBase
    {
        public PunishedLevelsController()
        {

        }
        public IActionResult GetPunishedLevel()
        {
            return Ok();
        }
        public IActionResult GetPunishedLevels(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreatePunishedLevel()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePunishedLevel(int id)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePunishedLevel(int id)
        {
            return Ok();
        }
    }
}
