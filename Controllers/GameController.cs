﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdleBotWeb.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
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
