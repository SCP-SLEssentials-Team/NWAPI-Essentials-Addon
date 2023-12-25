using CommandSystem;
using Newtonsoft.Json;
using NWAPI_Essentials.Commands;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NWAPI_Essentials_Addon.Commands
{
    internal class Warn : ICommand
    {
        private const string _log = "https://discord.com/api/webhooks/ADD_YOUR_WEBHOOK_URL";
        public static Warn Instance { get; } = new Warn();
        public string Command => "Warn";
        public string[] Aliases => new[] { "W" };
        public string Description => " Warning User for breaking rules";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = "You don't have permission to use this command! (Permission name: Overwatch)";
                return false;
            }

            if(arguments.Count < 1)
            {
                response = "Use this: PlayerID message";
                return false;
            }

            bool parsed = int.TryParse(arguments.At(0), out int playerId);
            if (!parsed)
            {
                response = "Invalid player ID provided.";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"No player found with ID: {playerId}";
                return false;
            }

            string message = string.Join(" ", arguments.ToArray());
            var playerSender = sender as PlayerCommandSender;
            string ply = playerSender.Nickname;

            var json = new
            {
                NickName = ply,
                warn = "warned",
                WarnedUser = player.Nickname,
                WarnedUserSteamID64 = player.UserId,
                reason = $"for {message}"
            };

            player.SendBroadcast($"<color=red>You warned for {message}</color>", 5);
            using (var client = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(json);
                var content = new StringContent(JsonConvert.SerializeObject(new { content = jsonData }), Encoding.UTF8, "application/json");
                var result = client.PostAsync(_log, content).Result;
                response = $"Message sent!";
                return true;
            }
        }
    }
}