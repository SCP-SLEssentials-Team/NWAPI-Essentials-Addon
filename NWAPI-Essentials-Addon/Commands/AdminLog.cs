using CommandSystem;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using PluginAPI.Core;

namespace NWAPI_Essentials.Commands
{
    internal class AdminLog : ICommand
    {
        private string _log = "https://discord.com/api/webhooks/ADD_YOUR_WEBHOOK";
        public static AdminLog Instance { get; } = new AdminLog();
        public string Command { get; } = "Log";
        public string[] Aliases { get; } = { "L" };
        public string Description { get; } = "Log's for SCPSL";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = "You don't have permission to use this command! (Permission name: Overwatch)";
                return false;
            }

            foreach (Player ply in Player.GetPlayers())
            {
                string message = string.Join(" ", arguments.ToArray());
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(new { content = $"{ply.Nickname} {message}" }), Encoding.UTF8, "application/json");
                    var result = client.PostAsync(_log, content).Result;
                    response = $"Message sent!";
                    return true;
                }
            }
            response = null;
            return true;
        }
    }
}
