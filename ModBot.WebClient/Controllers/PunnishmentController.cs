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
       
        public IActionResult Settings(int id)
        {
            return View();
        }
    }
}
