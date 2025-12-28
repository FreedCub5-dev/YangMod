// Modules/YangStates.cs
using UnityEngine;

namespace YangMod.Modules
{
    // Minimal stub so other files can call YangStates.Init() safely.
    // Replace with your real states registration when ready.
    public static class YangStates
    {
        public static bool HasInit => true;
        public static void Init()
        {
            Debug.Log("[YangMod] YangStates.Init() called (stub).");
        }
    }
}
