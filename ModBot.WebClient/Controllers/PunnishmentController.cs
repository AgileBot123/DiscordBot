using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class PunnishmentController : Controller
    {
       public IActionResult Get_PunishLevel(int id)
        {
            return View();
        }
        public IActionResult GetList_PunishLevel()
        {
            return View();
        }
        public IActionResult Create_PunishLevel()
        {
            return View();
        }
        public IActionResult Delete_PunishLevel(int id)
        {
            return View();
        }
        public IActionResult Update_PunishLevel(int id)
        {
            return View();
        }
    }
}
