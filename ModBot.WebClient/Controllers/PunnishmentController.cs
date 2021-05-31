using Microsoft.AspNetCore.Mvc;
using ModBot.Domain.DTO;
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
    public class PunnishmentController : Controller
    {
        private readonly IEndpoints endpoints;
        public PunnishmentController()
        {
            endpoints = new Endpoints();
        }
       
       

        public IActionResult Settings(PunishmentModel punishmentModel)
        {
            
            return View();
        }
    }
}
