using RoR2;

namespace YangMod.Modules.BaseContent.Characters
{
    public abstract class ItemDisplaysBase
    {
        /// <summary>
        /// Implement this in your survivor-specific item display class
        /// to populate the ItemDisplayRuleSet with rules.
        /// </summary>
        public abstract void SetItemDisplays(ItemDisplayRuleSet itemDisplayRuleSet);
    }
}
