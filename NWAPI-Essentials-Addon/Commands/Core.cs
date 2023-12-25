using CommandSystem;
using NWAPI_Essentials.Commands;
using NWAPI_Essentials_Addon.Commands;
using System;

namespace NWAPI_Essentials_NWAPI_Essentials_Addon.Commands
{
    internal class Commands
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public class CommandsEs : ParentCommand
        {
            public CommandsEs() => LoadGeneratedCommands();

            public sealed override void LoadGeneratedCommands()
            {
                RegisterCommand(AdminLog.Instance);
                RegisterCommand(Warn.Instance);
            }

            protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                response = "\nPlease enter a valid subcommand:";

                foreach (ICommand command in AllCommands)
                {
                    response += $"\n\n<color=yellow><b>- {command.Command} ({string.Join(", ", command.Aliases)})</b></color>\n<color=white>{command.Description}</color>";
                }
                return false;
            }

            public override string Command { get; } = "eta";
            public override string[] Aliases { get; } = Array.Empty<string>();
            public override string Description { get; } = "EssentialsCommands.";
        }
    }
}
