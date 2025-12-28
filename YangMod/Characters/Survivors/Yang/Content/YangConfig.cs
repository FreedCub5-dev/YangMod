using BepInEx.Configuration;

namespace YangMod.Survivors.Yang.Content
{
    /// <summary>
    /// Yang-specific config entries. Self-contained (no template helpers required).
    /// </summary>
    public static class YangConfig
    {
        internal static ConfigEntry<bool> enableYang;
        internal static ConfigEntry<bool> enableDebugLogs;

        /// <summary>
        /// Call from YangSurvivor.Init() or plugin startup to bind entries.
        /// </summary>
        public static void Init(ConfigFile config)
        {
            // Prevent double-binding if Init gets called more than once.
            if (enableYang != null) return;

            enableYang = config.Bind(
                "01 - Yang",
                "Enable Survivor",
                true,
                "If false, Yang will not be registered/added to the survivor roster."
            );

            enableDebugLogs = config.Bind(
                "01 - Yang",
                "Enable Debug Logs",
                false,
                "If true, Yang will print extra logs for troubleshooting."
            );
        }

        public static bool Enabled => enableYang?.Value ?? true;
        public static bool Debug => enableDebugLogs?.Value ?? false;
    }
}
