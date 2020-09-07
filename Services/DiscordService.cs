using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;

namespace IdleBotWeb.Services
{
    public class DiscordService
    {
        private readonly DiscordRestClient _client;

        public DiscordService(string token)
        {
            _client = new DiscordRestClient();
            _client.LoginAsync(TokenType.Bot, token);
        }

        public RestUser GetUser(ulong id)
        {
            return _client.GetUserAsync(id).Result;
        }

        public string GetAvatarUrl(ulong playerId)
        {
            var player = _client.GetUserAsync(playerId).Result;
            var avatarUrl = player.GetAvatarUrl();
            if (string.IsNullOrWhiteSpace(avatarUrl))
            {
                avatarUrl = player.GetDefaultAvatarUrl();
            }

            return avatarUrl;
        }
    }
}
