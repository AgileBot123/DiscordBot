using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.WebClient.Models;
using ModBot.WebClient.Models.Endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ModBot.WebClient.Controllers
{
    public class BannedWordController : Controller
    {
        private readonly IEndpoints endpoints;

        public BannedWordController()
        {
            endpoints = new Endpoints();
        }

        [HttpGet]
        public IActionResult Get_BannedWords(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllBannedWords()
        {   
            var banned = new List<ListBannedWords>();
            using (HttpClient client = new HttpClient())
            {
            
            var response = client.GetAsync(new Uri(endpoints.GetAllBannedWords)).Result;

                var jsonstring = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ListOfBannedWordsDTO>(jsonstring);
                foreach (var item in res.BWords)
                {
                    var result = new ListBannedWords
                    {
                        Banned_Words = item.Word,
                        Penaltylevel = item.Punishment,
                        
                    };  
                        banned.Add(result);
                }
                    ViewBag.Message = banned;
                }       
            
            return View(banned);
        }
        public IActionResult Create_BannedWord()
        {

            return View("CreateBannedWord");
        }

        [HttpPost]
        public IActionResult Create_BannedWord(BannedWordModel bannedWordModel)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Delete_BannedWord(string id)
        {
            var test = new DeleteBannedWordModel()
            {
                Word = id,
            };
            Delete_BannedWord(test.ToString());
            return View("index");
        }
        
          
        [HttpPatch]
        public IActionResult Update_BannedWord(int id, string bannedword)
        {
            return View();
        }
         
    }
}
