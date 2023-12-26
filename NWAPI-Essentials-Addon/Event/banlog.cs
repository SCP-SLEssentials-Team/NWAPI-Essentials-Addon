using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Core;
using System.Net.Http;
using PluginAPI.Core.Interfaces;
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
                var serverName = "Your Server Name";
                var bannerNickname = player;

                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        username = bannedPlayer.Nickname,
                        content = bannerNickname.Nickname, bannerNickname.UserId, bannerNickname.IpAddress, serverName, reason, duration
                    };

                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = httpClient.PostAsync("https://discord.com/api/webhooks/ADD_YOUR_WEBHOOK_URL", httpContent);
                    responseTask.Wait();
                }
                {
                    Log.Debug("player in null");
                }
            }
        }
    }
}