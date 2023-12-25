using CommandSystem;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using RemoteAdmin;

namespace NWAPI_Essentials.Commands
{
    internal class AdminLog : ICommand
    {
        private string _log = "https://discord.com/api/webhooks/ADD_YOUR_WEBHOOK_URL";
        public static AdminLog Instance { get; } = new AdminLog();
        public string Command { get; } = "Log";
        public string[] Aliases { get; } = { "L" };
        public string Description { get; } = "Logs for SCPSL";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = "You don't have permission to use this command! (Permission name: Overwatch)";
                return false;
            }

            string message = string.Join(" ", arguments.ToArray());
            var playerSender = sender as PlayerCommandSender;
            if (playerSender != null)
            {
                message += $" ({playerSender.Nickname})";
            }

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { content = message }), Encoding.UTF8, "application/json");
                var result = client.PostAsync(_log, content).Result;
                response = $"Message sent!";
                return true;
            }
        }
    }
}
