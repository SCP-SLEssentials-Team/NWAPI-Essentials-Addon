using System.ComponentModel;

namespace NWAPI_Essentials_Addon
{
    public class Config
    {
        public Config() { }
        [Description("Active Essentials Addon?.")]

        public bool IsEnabled { get; set; } = true;
        [Description("Active auto log bans?.")]

        public bool log_bans { get; set; } = true;
    }
}
