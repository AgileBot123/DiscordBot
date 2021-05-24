using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModBot.WebClient.Controllers
{
    public class UserLogginController : Controller
    {
       private readonly string token  = "place token string here"; 
        public IActionResult Index()
        {
            return View();
        }

        public string GetToken(string username, string password)
        {
            using (var _httpClient = new HttpClient())
            {
                var _userdic = new Dictionary<string, string>
                {
                    {"username", username },
                    {"password", password },
                    {"grant_type", "password"}

                };
                var content = new FormUrlEncodedContent(_userdic);
                var response = _httpClient.PostAsync(token, content).Result;
                var jsonstring = response.Content.ReadAsStringAsync().Result;
                //var tokenstring = JsonConvert.DeserializeObject<TokenDTO>(jsonstring);  // behöver en Token 
                //return tokenstring.access_token; 
                return null;
            }
           
        }
        [HttpGet]
        public IActionResult Loggin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Loggin(LoginDTO model)
        {
            var token = GetToken(model.username, model.password);
            if(string.IsNullOrEmpty(token))
            {
                return RedirectToAction("index");
            }
            //Session["tokenkey"] = token;
            return RedirectToAction("index");
            
        }
    }
}
