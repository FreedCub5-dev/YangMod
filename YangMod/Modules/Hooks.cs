namespace YangMod.Modules
{
    public static class Hooks
    {
        public static void Init()
        {
            // Phase 4 stability: no hooks.
            YangPlugin.Log.LogInfo("[Hooks] Init (no hooks installed).");
        }
    }
}
