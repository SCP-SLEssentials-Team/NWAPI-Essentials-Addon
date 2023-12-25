using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Core;
using System.Net.Http;
using PluginAPI.Core.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System;

namespace NWAPI_Essentials_Addon.Event
{
    internal class BanLog
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerBanned)]
        public void LogBan(IPlayer player, Player bannedPlayer, string reason, Int64 duration)
        {
            if (player != null)
            {
                var serverName = "Essentials-Test";
                var bannerNickname = player;

                var json = new
                {
                    Nickname = bannedPlayer.Nickname,
                    Reason = reason,
                    Duration = duration,
                    Server = serverName,
                    BannedUser = bannerNickname.Nickname,
                    SteamID64BannedUser = bannerNickname.UserId,
                    IPadressBannedUser = bannerNickname.IpAddress
                };
                using (var client = new HttpClient())
                {
                    var jsonData = JsonConvert.SerializeObject(json);
                    var content = new StringContent(JsonConvert.SerializeObject(new { content = jsonData }), Encoding.UTF8, "application/json");
                    var result = client.PostAsync("https://discord.com/api/webhooks/ADD_YOUR_WEBHOOK_URL", content).Result;
                }
            }
            else
            {
                Log.Error("Player is null");
            }
        }
    }
}