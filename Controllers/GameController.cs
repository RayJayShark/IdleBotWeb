using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IdleBotWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdleBotWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly DiscordService _discordService;

        public GameController(DatabaseService databaseService, DiscordService discordService)
        {
            _databaseService = databaseService;
            _discordService = discordService;
        }

        public IActionResult Index()
        {
            var players = _databaseService.GetAllPlayers();
            foreach (var p in players)
            {
                if (string.IsNullOrWhiteSpace(p.Avatar))
                {
                    p.Avatar =_discordService.GetAvatarUrl(p.Id);
                    _databaseService.UpdateAvatar(p.Id, p.Avatar);
                }
            }

            ViewBag.Players = players;
            return View();
        }

        public IActionResult Shop()
        {
            ViewBag.Items = _databaseService.GetItems();
            
            return View();
        }

        public IActionResult Attack()
        {
            return View();
        }

        public IActionResult Trade()
        {
            return View();
        }

        public IActionResult Party()
        {
            return View();
        }
    }
}
