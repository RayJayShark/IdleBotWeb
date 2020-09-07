using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Rest;

namespace IdleBotWeb.Services
{
    public class DiscordService
    {
        private readonly DiscordRestClient _client;

        public DiscordService()
        {
            _client = new DiscordRestClient();
        }

        public RestUser GetUser(ulong id)
        {
            return _client.GetUserAsync(id).Result;
        }
    }
}
