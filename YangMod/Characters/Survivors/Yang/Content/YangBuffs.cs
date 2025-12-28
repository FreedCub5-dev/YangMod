// Modules/YangBuffs.cs
using RoR2;
using UnityEngine;

namespace YangMod.Modules
{
    public static class YangBuffs
    {
        public static BuffDef YangSemblanceBuff;

        public static void CreateBuffs()
        {
            if (YangSemblanceBuff != null) return;

            YangSemblanceBuff = ScriptableObject.CreateInstance<BuffDef>();
            YangSemblanceBuff.name = "YANG_SEMBLANCE_BUFF";
            YangSemblanceBuff.iconSprite = null; // assign via Assets later if you have an icon
            YangSemblanceBuff.buffColor = new Color32(255, 150, 50, 255);
            YangSemblanceBuff.isDebuff = false;
            YangSemblanceBuff.canStack = true;
        }
    }
}
