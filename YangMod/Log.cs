using BepInEx.Logging;

namespace YangMod.Modules
{
    public static class Log
    {
        internal static ManualLogSource Logger;

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        public static void Info(object data) => Logger?.LogInfo(data);
        public static void Warning(object data) => Logger?.LogWarning(data);
        public static void Error(object data) => Logger?.LogError(data);
        public static void Debug(object data) => Logger?.LogDebug(data);
    }
}
