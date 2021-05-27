using Microsoft.AspNetCore.Mvc;
using ModBot.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class PunnishmentController : Controller
    {
        public PunnishmentController()
        {

        }
        public IActionResult CreatePunishemnt()
        {
            return View();
        }
        public IActionResult CreatePunishemnt(PunishmentModel punishmentModel)
        {
            return View();
        }
        public IActionResult Get_PunishLevel(int id)
        {
            return View();
        }
        public IActionResult GetList_PunishLevel()
        {
            return View();
        }
        public IActionResult DeletePunishment(int id)
        {
            return View();
        }
        public IActionResult UpdatePunishment(int id)
        {
            return View();
        }

        public IActionResult Settings(int id)
        {
            return View();
        }
    }
}
