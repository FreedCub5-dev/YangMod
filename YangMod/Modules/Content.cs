using RoR2;
using UnityEngine;

namespace YangMod.Modules
{
    /// <summary>
    /// Small "content helper" module used by the template.
    /// Your other modules can call into this as needed.
    /// </summary>
    public static class Content
    {
        public static void Init()
        {
            // Intentionally minimal: this project is being brought back to a compiling state.
            // You can expand this later (sound defs, effect defs, etc.).
        }

        /// <summary>
        /// Creates a NetworkSoundEventDef and queues it into the content pack.
        /// Avoids a hard dependency on AkSoundEngine at compile time.
        /// </summary>
        public static NetworkSoundEventDef CreateAndAddNetworkSoundEventDef(string eventName)
        {
            var def = ScriptableObject.CreateInstance<NetworkSoundEventDef>();
            def.eventName = eventName;

            // If AkSoundEngine is available at runtime, you can set akId there.
            // We keep this 0 for compilation portability.
            def.akId = 0;

            return def;
        }
    }
}
