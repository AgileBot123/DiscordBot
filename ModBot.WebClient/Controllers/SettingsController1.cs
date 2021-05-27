using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class SettingsController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       [HttpGet]
        public IActionResult settings()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Settings(SettingsDTO model)
        {
            var settings = new SettingsModel()
            {
                Insert_Word = model.Insert_Word,
                BadWord1 = model.BadWord1,
                BadWord2 = model.BadWord2,
                BadWord3 = model.BadWord3,
                Bad_Sentence1 = model.Bad_Sentence1,
                Bad_Sentence2 = model.Bad_Sentence2

            };

            
            return View();
        }
    }
}
