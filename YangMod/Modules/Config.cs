using BepInEx.Configuration;
using UnityEngine;

namespace YangMod.Modules
{
    /// <summary>
    /// Centralized config bindings for the mod. Call Init(...) once from YangPlugin.Awake().
    /// </summary>
    public static class Config
    {
        internal static ConfigFile ConfigFile;

        // Example toggles / keys used across the template
        public static ConfigEntry<bool> EnableSurvivor;
        public static ConfigEntry<KeyCode> PrimaryKey;
        public static ConfigEntry<KeyCode> SecondaryKey;
        public static ConfigEntry<KeyCode> UtilityKey;
        public static ConfigEntry<KeyCode> SpecialKey;

        // If you have more settings in other files, bind them here.
        public static void Init(ConfigFile config)
        {
            ConfigFile = config;

            EnableSurvivor = config.Bind("01 - General", "Enable Survivor", true,
                "If false, Yang will not be registered/added to the survivor catalog.");

            PrimaryKey = config.Bind("02 - Keys", "Primary", KeyCode.Mouse0, "Primary skill key (mostly for debugging).");
            SecondaryKey = config.Bind("02 - Keys", "Secondary", KeyCode.Mouse1, "Secondary skill key (mostly for debugging).");
            UtilityKey = config.Bind("02 - Keys", "Utility", KeyCode.LeftShift, "Utility skill key (mostly for debugging).");
            SpecialKey = config.Bind("02 - Keys", "Special", KeyCode.Q, "Special skill key (mostly for debugging).");
        }

        // Convenience helpers (optional)
        public static bool PrimaryPressedThisFrame() => Input.GetKeyDown(PrimaryKey.Value);
        public static bool SecondaryPressedThisFrame() => Input.GetKeyDown(SecondaryKey.Value);
        public static bool UtilityPressedThisFrame() => Input.GetKeyDown(UtilityKey.Value);
        public static bool SpecialPressedThisFrame() => Input.GetKeyDown(SpecialKey.Value);
    }
}
