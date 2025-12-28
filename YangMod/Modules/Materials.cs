using UnityEngine;

namespace YangMod.Modules
{
    internal static class Materials
    {
        internal static void Init()
        {
            // keep empty for now
        }

        internal static Material LoadMaterial(string resourcesPath)
        {
            return Resources.Load<Material>(resourcesPath);
        }
    }
}
