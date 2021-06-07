using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.WebClient.Extention;
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

        public IActionResult Index()
        {
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        public IActionResult SaveSettings(SettingsDTO settingsDTO)
        {
            var settings = Session.Get<SettingsDTO>(HttpContext.Session, "settings");
            try
            {
                var bannedWordListDto = new BannedWordListDto();
                bannedWordListDto.GuildId = Session.Get<ulong>(HttpContext.Session, "guild");
                foreach(var bannedWord in settings.BannedWordList)
                {
                    bannedWordListDto.BannedWordList.Add(bannedWord);
                }
                UpdateBannedWordList(bannedWordListDto);

                var punishmentSettingsDto = new PunishmentSettingsDto()
                {
                    TimeOutLevel = settingsDTO.TimeOutLevel,
                    KickLevel = settingsDTO.KickLevel,
                    BanLevel = settingsDTO.BanLevel,
                    SpamMuteTime = settingsDTO.SpamMuteTime,
                    StrikeMuteTime = settingsDTO.StrikeMuteTime,
                    GuildId = Session.Get<ulong>(HttpContext.Session, "guild")
                };
                UpdatePunishmentSettings(punishmentSettingsDto);

                settings.TimeOutLevel = settingsDTO.TimeOutLevel;
                settings.KickLevel = settingsDTO.KickLevel;
                settings.BanLevel = settingsDTO.BanLevel;
                settings.SpamMuteTime = settingsDTO.SpamMuteTime;
                settings.StrikeMuteTime = settingsDTO.StrikeMuteTime;
                Session.Set<SettingsDTO>(HttpContext.Session, "settings", settings);

                ViewBag.successResponse = "Your settings have been saved!";
                ViewBag.failureResponse = "";
                return View("Settings", settings);
            }
            catch (Exception ex)
            {
                ViewBag.successResponse = "";
                ViewBag.failureResponse = "Your settings failed to save.";
                return View("Settings", settings);
            }
        }

        public IActionResult AddProfanity(BannedWordDto addedBannedWord)
        {
            var Settings = Session.Get<SettingsDTO>(HttpContext.Session, "settings");

            addedBannedWord.BannedWordUsedCount = 0;
            addedBannedWord.GuildId = Session.Get<ulong>(HttpContext.Session, "guild");

            if(Settings.BannedWordList.Any(w => w.Profanity.ToLower() == addedBannedWord.Profanity.ToLower()))
            {
                var removedBannedWord = Settings.BannedWordList.Where(
                    w => w.Profanity.ToLower() == addedBannedWord.Profanity.ToLower()).Single();
                Settings.BannedWordList.Remove(removedBannedWord);
            }
            Settings.BannedWordList.Add(addedBannedWord);

            Session.Set<SettingsDTO>(HttpContext.Session, "settings", Settings);
            
            return View("Settings", Settings);
        }

        public IActionResult UpdateProfanity(BannedWordDto inputBannedWord)
        {
            var Settings = Session.Get<SettingsDTO>(HttpContext.Session, "settings");
            var BannedWord = Settings.BannedWordList.Where(
                    w => w.Profanity.ToLower() == inputBannedWord.Profanity.ToLower()).FirstOrDefault();

            if (inputBannedWord.SubmitType.Equals("remove"))
            {
                Settings.BannedWordList.Remove(BannedWord);
            }
            else
            {
                var updatedBannedWord = BannedWord;
                updatedBannedWord.Strikes = inputBannedWord.Strikes;
                updatedBannedWord.Punishment = inputBannedWord.Punishment;

                Settings.BannedWordList.Remove(BannedWord);
                Settings.BannedWordList.Add(updatedBannedWord);
            }
            Session.Set<SettingsDTO>(HttpContext.Session, "settings", Settings);

            return View("Settings", Settings);
        }

        [Authorize(AuthenticationSchemes = "Discord")]
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var guildId = Session.Get<ulong>(HttpContext.Session, "guild");
            if (guildId != 0)
            {
                HandleCookie("ServerIsSelected", "true");
                var allBannedWords = await GetAllBannedWord(guildId);
                var PunishmentLevels = await GetPunishmentLevels(guildId);

                var settings = new SettingsDTO()
                {
                    BannedWordList = allBannedWords,
                    TimeOutLevel = PunishmentLevels.TimeOutLevel,
                    KickLevel = PunishmentLevels.KickLevel,
                    BanLevel = PunishmentLevels.BanLevel,
                    SpamMuteTime = PunishmentLevels.SpamMuteTime,
                    StrikeMuteTime = PunishmentLevels.StrikeMuteTime
                };

                Session.Set<SettingsDTO>(HttpContext.Session, "settings", settings);

                return View(settings);
            }
            else 
                return RedirectToAction("ServerList", "Authentication");
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
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        [HttpPost]
        public async Task<PunishmentSettingsDto> GetPunishmentLevels(ulong guildId)
        {

            using (HttpClient client = new HttpClient())
            {
                var punishmentSettings = new PunishmentSettingsDto() { GuildId = guildId };
                var JsonString = JsonConvert.SerializeObject(punishmentSettings);
                var stringContent = new StringContent(JsonString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoints.GetPunishmentLevels, stringContent).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    punishmentSettings = JsonConvert.DeserializeObject<PunishmentSettingsDto>(jsonString);
                    return punishmentSettings;
                }
            }

            HandleCookie("ServerIsSelected", "true");
            return null;

        }


        [HttpGet]
        public IActionResult GetPunishLevel(int id)
        {
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        [HttpGet]
        public IActionResult DeletePunishment(int? id)
        {

            using (HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetPunishmentLevel + id;
                var response = client.GetAsync(requestUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var punishment = JsonConvert.DeserializeObject<PunishmentSettingsDto>(jsonString);
                    return View(punishment);
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        [HttpPost]
        public IActionResult DeletePunishment(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.DeletePunishmentLevel + id;
                var response = client.DeleteAsync(requestUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Settings");
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        [HttpGet]
        public IActionResult UpdatePunishment(int? id)
        {

            using (HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetPunishmentLevel + id;
                var response = client.GetAsync(requestUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var punishment = JsonConvert.DeserializeObject<PunishmentSettingsDto>(jsonString);
                    return View(punishment);
                }
            }
            HandleCookie("ServerIsSelected", "true");

            return View();
        }

        [HttpPost]
        public void UpdatePunishmentSettings(PunishmentSettingsDto punishmentSettingsDto)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(punishmentSettingsDto);
                var stringContent = new StringContent(jsonString, Encoding.UTF8, "Application/json");
                var response = client.PostAsync(endpoints.UpdatePunishmentLevel, stringContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Punishment settings failed to update in database");
                }
            }
        }
        
        [HttpPost]
        public async Task<List<BannedWordDto>> GetAllBannedWord(ulong guildId)
        {
            var bannedWords = new List<BannedWordDto>();
            using(HttpClient client = new HttpClient())
            {
                var bannedwordDto = new BannedWordDto() { GuildId = guildId };
                var JsonString = JsonConvert.SerializeObject(bannedwordDto);
                var stringContent = new StringContent(JsonString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoints.GetAllBannedWords, stringContent).Result;
                if(response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    bannedWords = JsonConvert.DeserializeObject<List<BannedWordDto>>(jsonString);
                    return bannedWords;
                }
                return null;
            }
        }



        [HttpGet]
        public IActionResult CreateBannedWord()
        {
            HandleCookie("ServerIsSelected", "true");
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

            ViewBag.Message = "Banned word created";
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        public IActionResult DeleteBannedWord(int? id)
        {
            
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetBannedWord + id;
                var response = client.GetAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var bannedWord = JsonConvert.DeserializeObject<BannedWordDto>(jsonString);
                    return View(bannedWord);
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        
        public IActionResult DeleteBannedWord(int id)
        { 

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
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        [HttpGet]
        public IActionResult UpdateBannedWord(string word)
        {

            using (HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetBannedWord + word;
                var response = client.GetAsync(requestUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var bannedWord = JsonConvert.DeserializeObject<BannedWordDto>(jsonString);
                    return View(bannedWord);
                }
            }
            HandleCookie("ServerIsSelected", "true");

            return View();
        }

        [HttpPost]
        public void UpdateBannedWordList(BannedWordListDto bannedWordListDto)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(bannedWordListDto);
                var stringContent = new StringContent(jsonString, Encoding.UTF8, "Application/json");
                var response = client.PutAsync(endpoints.UpdateBannedWordList, stringContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Banned word list failed to update in database");
                }
            }
        }

        public void HandleCookie(string name, string content)
        {
            HttpContext.Response.Cookies.Append(name, content, new CookieOptions()
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))

            });
        }

    }
}
