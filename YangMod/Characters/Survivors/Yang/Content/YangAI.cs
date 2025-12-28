using UnityEngine;
using YangMod.Modules;

namespace YangMod.Survivors.Yang.Content
{
    internal static class YangAI
    {
        internal static GameObject CreateMasterPrefab(GameObject bodyPrefab)
        {
            return Prefabs.CreateBlankMasterPrefab("YangMonsterMaster", bodyPrefab);
        }
    }
}
