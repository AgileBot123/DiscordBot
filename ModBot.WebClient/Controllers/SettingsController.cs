using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using ModBot.Domain.DTO.BannedWordDto;
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

        public IActionResult CreatePunishemnt()
        {
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        [HttpPost]
        public IActionResult CreatePunishemnt(PunishmentModel punishmentModel)
        {
            if (ModelState.IsValid)
            {
                var createPunishmentRequest = new PunishmentDto
                {
                    TimeOutLevel = punishmentModel.TimeOutLevel,
                    KickLevel = punishmentModel.KickLevel,
                    BanLevel = punishmentModel.BanLevel,
                    SpamMuteTime = punishmentModel.SpamMuteTime,
                    StrikeMuteTime = punishmentModel.StrikeMuteTime
                };
                string jsonCreatePunishment = JsonConvert.SerializeObject(createPunishmentRequest);
                var httpContet = new StringContent(jsonCreatePunishment, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = client.PostAsync(new Uri(endpoints.CreatePunishmentLevel), httpContet).Result;
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        return View("Error");
                }
                return RedirectToAction("Index");

            }
            ViewBag.Message = "New Punishment";
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        public IActionResult GetAllPunishmentLevel()
        {
            var punishmentList = new List<PunishmentModel>();
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(endpoints.GetPunishmentLevels).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var punishmentResponse = JsonConvert.DeserializeObject<PunishmentLevelsListDto>(jsonString);
                    foreach (var punishment in punishmentResponse.Punishments)

                    {
                        var punishmentModel = new PunishmentModel
                        {
                            TimeOutLevel = punishment.TimeOutLevel,
                            KickLevel = punishment.KickLevel,
                            BanLevel = punishment.BanLevel,
                            SpamMuteTime = punishment.SpamMuteTime,
                            StrikeMuteTime = punishment.StrikeMuteTime
                        };
                        punishmentList.Add(punishmentModel);
                    }
                    ViewBag.Punishments = punishmentList;
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View("punisments", punishmentList);
        }
        public IActionResult GetPunishLevel(int id)
        {
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        public IActionResult DeletePunishment(int? id)
        {
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetPunishmentLevel + id;
                var response = client.GetAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var punishment = JsonConvert.DeserializeObject<PunishmentDto>(jsonString);
                    return View(punishment);
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        public IActionResult DeletePunishment(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.DeletePunishmentLevel + id;
                var response = client.DeleteAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction();
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }

        public IActionResult UpdatePunishment(int? id)
        {
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetPunishmentLevel + id;
                var response = client.GetAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var punishment = JsonConvert.DeserializeObject<PunishmentDto>(jsonString);
                    return View(punishment);
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        public IActionResult UpdatePunishment(PunishmentModel update)
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
                        return RedirectToAction();
                    }
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        public IActionResult GetAllBannedWord()
        {
            var bannedWords = new List<BannedWordModel>();
            using(HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(endpoints.GetAllBannedWords).Result;
                if(response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var bannedWordResponse = JsonConvert.DeserializeObject<List<BannedWordDto>>(jsonString);
                    foreach (var bannedword in bannedWordResponse)
                    {
                        var bannedWordListModel = new BannedWordModel
                        {
                            Word = bannedword.Word,
                            Strikes = bannedword.Strikes,
                            Punishment = bannedword.Punishment,
                             
                        };
                        bannedWords.Add(bannedWordListModel);
                    }
                    ViewBag.Message = bannedWords;
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View(bannedWords);
        }
        public IActionResult CreateBannedWord()
        {
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        public IActionResult CreateBannedWord(BannedWordModel bannedWordModel)
        {
            if(ModelState.IsValid)
            {
                var createBannedWordRequest = new BannedWordDto
                {
                    Word = bannedWordModel.Word,
                    Strikes = bannedWordModel.Strikes,
                    Punishment = bannedWordModel.Punishment
                };
                string jsonCreateBannedWord = JsonConvert.SerializeObject(createBannedWordRequest);
                var Content = new StringContent(jsonCreateBannedWord, Encoding.UTF8, "application/json");

                using(HttpClient client = new HttpClient())
                {
                    var response = client.PostAsync(new Uri(endpoints.CreateBannedWord), Content).Result;
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        return View("Error");
                }
                return RedirectToAction();
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
                      
            using(HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.DeleteBannedWord + id;
                var response = client.DeleteAsync(requestUrl).Result;
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction();
                }
            }
            HandleCookie("ServerIsSelected", "true");
            return View();
        }
        public IActionResult UpdateBannedWord(int? id)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestUrl = endpoints.GetBannedWord + id;
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
        public IActionResult UpdateBannedWord(BannedWordModel update)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
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
            HandleCookie("ServerIsSelected", "true");
            return View();
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
