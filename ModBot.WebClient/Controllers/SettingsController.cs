using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.WebClient.Models;
using ModBot.WebClient.Models.Endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IEndpoints endpoints;
        public SettingsController()
        {
            endpoints = new Endpoints();
        }

        [HttpGet]
        public IActionResult Settings()
        {

           var allBannedWords = GetAllBannedWord();
           var allPunishmentLevels = GetAllPunishmentLevel();

            var settings = new List<SettingsDTO>();


            foreach (var words in allBannedWords)
            {
                foreach (var punishments in allPunishmentLevels)
                {
                    settings.Add(new SettingsDTO()
                    {
                        BannedWordList = allBannedWords, 
                        TimeOutLevel = punishments.TimeOutLevel,
                        KickLevel = punishments.KickLevel,
                        BanLevel = punishments.BanLevel,
                        SpamMuteTime = punishments.SpamMuteTime,
                        StrikeMuteTime = punishments.StrikeMuteTime
                    });;
                }
            }

            return View(settings);
        }

        //[HttpPost]
        //public IActionResult Settings(//SUperMOdel Parent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CreateBannedWord(new BannedWordDto(//parent.BannedWord.Profanity, parent....));
        //        CreatePunishemnt(new PunishmentDto(//parent.Punishment.Id, parent....));
        //    }

        //    return View();
        //}

        [HttpPost]
        public IActionResult CreatePunishemnt(PunishmentSettingsDto punishmentModel)
        {
            if (ModelState.IsValid)
            {
                string jsonCreatePunishment = JsonConvert.SerializeObject(punishmentModel);
                var httpContet = new StringContent(jsonCreatePunishment, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = client.PostAsync(new Uri(endpoints.CreatePunishmentLevel), httpContet).Result;
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        return View("Error");
                }
                return RedirectToAction("Settings");

            }
            ViewBag.Message = "New Punishment";
            return View();
        }

        public List<PunishmentSettingsDto> GetAllPunishmentLevel()
        {
            var punishmentList = new List<PunishmentSettingsDto>();
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(endpoints.GetPunishmentLevels).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    punishmentList = JsonConvert.DeserializeObject<List<PunishmentSettingsDto>>(jsonString);
                    return punishmentList;
                }
            }
            return null;
        }


        [HttpGet]
        public IActionResult GetPunishLevel(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeletePunishment(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeletePunishment(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.DeletePunishmentLevel + id;
                var response = client.DeleteAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Settings");
                }
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult UpdatePunishment(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePunishment(PunishmentSettingsDto update)
        {
            if(ModelState.IsValid)
            {
                using(HttpClient client = new HttpClient())
                {
                    var updatePunishment = JsonConvert.SerializeObject(update);
                    var content = new StringContent(updatePunishment, Encoding.UTF8, "Application/json");
                    var requestUrl = endpoints.UpdatePunishmentLevel;
                    var response = client.PutAsync(requestUrl, content).Result;
                    if(response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Settings");
                    }
                }
            }
            return View();
        }
        

        public List<BannedWordDto> GetAllBannedWord()
        {
            var bannedWords = new List<BannedWordDto>();
            using(HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(endpoints.GetAllBannedWords).Result;
                if(response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    bannedWords = JsonConvert.DeserializeObject<List<BannedWordDto>>(jsonString);
                    return bannedWords;
                }
            }
            return null;
        }



        [HttpGet]
        public IActionResult CreateBannedWord()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBannedWord(BannedWordDto bannedWord)
        {
            if(ModelState.IsValid)
            {
                var jsonCreateBannedWord = JsonConvert.SerializeObject(bannedWord);
                var Content = new StringContent(jsonCreateBannedWord, Encoding.UTF8, "application/json");

                using(HttpClient client = new HttpClient())
                {
                    var response = client.PostAsync(endpoints.CreateBannedWord, Content).Result;
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        return View("Error");
                    }            
                }
                return RedirectToAction("Settings");
            }
            ViewBag.Message = "Banned word Created";
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBannedWord(string word)
        {
                      
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.DeleteBannedWord + word;
                var response = client.DeleteAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {                    
                       return RedirectToAction("GetAllBannedWord");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateBannedWord(string word)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateBannedWord(BannedWordListDto update)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {

                    var bannedWord = new BannedWordDto();
                    update.BannedWordList.Add(bannedWord);

                    var updateBannedWord = JsonConvert.SerializeObject(update);
                    var content = new StringContent(updateBannedWord, Encoding.UTF8, "Application/json");
                    var requestUrl = endpoints.UpdateBannedWordList;
                    var response = client.PutAsync(requestUrl, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction();
                    }
                }
            }
            return View();
        }
    }
}
