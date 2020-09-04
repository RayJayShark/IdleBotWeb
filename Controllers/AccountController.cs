using System;
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

        public AccountController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
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
            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.Player = _databaseService.GetPlayer(ulong.Parse(User.Claims.First().Value));
            }
            else
            {
                ViewBag.Player = _databaseService.GetPlayer(ulong.Parse(id));
            }

            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
