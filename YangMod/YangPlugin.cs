using BepInEx;
using BepInEx.Logging;
using R2API.Utils;

namespace YangMod
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency("com.bepis.r2api.content_management", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class YangPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.brett.YangMod";
        public const string PluginName = "YangMod";
        public const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            Modules.Log.Init(Logger);
            Modules.Config.Init(Config);
            Modules.Hooks.Init();

            // ✅ ONLY register content provider here.
            Modules.ContentPacks.Init();
            Log.LogInfo("[YangMod] ContentPacks provider registered.");
            Log.LogInfo($"{PluginName} v{PluginVersion} loaded.");
            Logger.LogInfo($"[YangMod] Loaded from: {System.Reflection.Assembly.GetExecutingAssembly().Location}");
        }
    }
}
