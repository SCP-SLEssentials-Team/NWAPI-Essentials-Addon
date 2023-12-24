﻿using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace NWAPI_Essentials_Addon
{
    internal class Plugins
    {
        public static Plugins Singletion { get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials-Addon", "1.0.0", "Add more commands? Yes, it adds new commands that can't be added for some reason and have to be done manually, or it's Experimental commands", "Jevil")]

        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
                return;
            Singletion = this;
        }
    }
}
