using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace NWAPI_Essentials_Addon
{
    internal class Plugins
    {
        public static Plugins Singletion { get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials-Addon", "1.0.1", "Add more commands? Yes, it adds new commands that can't be added for some reaso and have to be done manually, or it's Experimental commands", "Jevil")]

        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
                return;
            Singletion = this;
            EventManager.RegisterEvents<Event.BanLog>(this);
        }
    }
}
