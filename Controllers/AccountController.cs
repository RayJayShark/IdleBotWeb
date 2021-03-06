﻿using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using IdleBotWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace IdleBotWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly DiscordService _discordService;

        public AccountController(DatabaseService databaseService, DiscordService discordService)
        {
            _databaseService = databaseService;
            _discordService = discordService;
        }
        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Profile");
            }
            return Challenge(new AuthenticationProperties { RedirectUri = "/Account/Profile/" }, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/");
            }
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult Profile(string id)
        {
            var player = _databaseService.GetPlayer(string.IsNullOrWhiteSpace(id) ? ulong.Parse(User.Claims.First().Value) : ulong.Parse(id));
            if (string.IsNullOrWhiteSpace(player.Avatar))
            {
                player.Avatar = _discordService.GetAvatarUrl(player.Id);
                _databaseService.UpdateAvatar(player.Id, player.Avatar);
            }

            ViewBag.Player = player;
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
