using System;
using UnityEngine;
using RoR2;

namespace YangMod.Modules
{
    /// <summary>
    /// Very small, safe stub of the item-display helper used by ItemDisplaysBase.
    /// It’s enough for the base classes to run and can be expanded later if you
    /// want proper item display prefabs.
    /// </summary>
    internal static class ItemDisplays
    {
        /// <summary>
        /// In the full template this lazily builds a cache of display prefabs.
        /// For now it’s just a no-op so the game doesn’t crash.
        /// </summary>
        internal static void LazyInit()
        {
            // Intentionally left blank – add real display prefab loading later.
        }

        /// <summary>
        /// In the full template this would clean up any temporary objects.
        /// We don’t allocate anything yet, so this is a no-op.
        /// </summary>
        internal static void DisposeWhenDone()
        {
            // Intentionally left blank.
        }

        /// <summary>
        /// In the full template this looks up / instantiates an item display prefab
        /// from an AssetBundle or LegacyResourcesAPI. For now we just return null
        /// so callers can safely skip creating rules.
        /// </summary>
        internal static GameObject LoadDisplay(string childName)
        {
            // TODO: Once you’re ready to set up real item displays, implement this
            // using your asset bundle or RoR2.LegacyResourcesAPI.
            return null;
        }
    }
}
