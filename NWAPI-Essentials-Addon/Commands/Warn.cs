using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Linq;
using System.Net.Http;

namespace NWAPI_Essentials_Addon.Commands
{
    internal class Warn : ICommand
    {
        private const string _log = "https://discord.com/api/webhooks/1126050696148566096/NO1dxGsIDqu40oc2HqUGyUZlfA0DAm6sw9-Oty3cGEQ-2EZpKqQjyYP_oInCPeHxZRez";
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

            player.SendBroadcast($"<color=red>You received a warn for {message}", 5);
            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    username = ply,
                    content = $"{player.Nickname}, {player.UserId}, received a warning for {message}",
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                var responseTask = httpClient.PostAsync(_log, httpContent);
                responseTask.Wait();

                if (responseTask.Result.IsSuccessStatusCode)
                {
                    response = "Message sent!";
                    return true;
                }
                else
                {
                    response = "Failed to send message.";
                    return false;
                }
            }
        }
    }
}