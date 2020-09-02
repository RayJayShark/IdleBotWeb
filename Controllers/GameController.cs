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

        public GameController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IActionResult Index()
        {
            ViewBag.Players = _databaseService.GetAllPlayers();

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
