// Achievements/YangMasteryAchievement.cs
using RoR2;
using RoR2.Achievements;
using UnityEngine;

namespace YangMod.Achievements
{
    /// <summary>
    /// Compile-safe mastery achievement stub.
    /// Register this achievement using your mod's registration flow (R2API or template).
    /// </summary>
    public class YangMasteryAchievement : BaseAchievement
    {
        // Do not override Identifier unless your RoR2 version declares it virtual.
        // Provide install/uninstall stubs for later implementation.
        public override void OnInstall()
        {
            base.OnInstall();
            Debug.Log("[YangMod] YangMasteryAchievement installed (stub).");
        }

        public override void OnUninstall()
        {
            base.OnUninstall();
            Debug.Log("[YangMod] YangMasteryAchievement uninstalled (stub).");
        }

        // If you need an identifier constant for registration, use this:
        public const string AchievementID = "YANG_MASTERY_ACHIEVEMENT";
    }

    public class YangMasteryServerAchievement : BaseServerAchievement
    {
        public override void OnInstall()
        {
            base.OnInstall();
            Debug.Log("[YangMod] YangMasteryServerAchievement installed (stub).");
        }

        public override void OnUninstall()
        {
            base.OnUninstall();
            Debug.Log("[YangMod] YangMasteryServerAchievement uninstalled (stub).");
        }
    }
}
