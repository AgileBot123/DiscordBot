using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class BannedWordController : Controller
    {
        [HttpGet]
        public IActionResult Get_BannedWords(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Get_AllBannedWords()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create_BannedWord()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete_BannedWord(int id)
        {
            return View();
        }
        [HttpPatch]
        public IActionResult Update_BannedWord(int id, string bannedword)
        {
            return View();
        }
         
    }
}
