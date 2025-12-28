using RoR2;
using YangMod.Modules.BaseContent.Characters;

namespace YangMod.Survivors.Yang
{
    public class YangItemDisplays : ItemDisplaysBase
    {
        // This matches the abstract member your template is requiring:
        // ItemDisplaysBase.SetItemDisplays(ItemDisplayRuleSet)
        public override void SetItemDisplays(ItemDisplayRuleSet ruleSet)
        {
            // compile-first: no displays yet
            // Later we'll populate ruleSet.keyAssetRuleGroups / ruleSet.namedItemRuleGroups etc
        }
    }
}
