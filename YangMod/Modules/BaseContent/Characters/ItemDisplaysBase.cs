// ItemDisplaysBase.cs - updated namespace and made SetItemDisplays virtual
using YangMod.Modules;
using RoR2;
using System.Collections.Generic;
using YangMod.Modules; // ensure access to ItemDisplays helper in YangMod.Modules

namespace YangMod.Modules.Characters
{
    public abstract class ItemDisplaysBase
    {
        /// <summary>
        /// Default SetItemDisplays implementation. Concrete classes can override this.
        /// It will populate the ItemDisplayRuleSet by calling SetItemDisplayRules.
        /// </summary>
        public virtual void SetItemDisplays(ItemDisplayRuleSet itemDisplayRuleSet)
        {
            List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();

            ItemDisplays.LazyInit();

            SetItemDisplayRules(itemDisplayRules);

            itemDisplayRuleSet.keyAssetRuleGroups = itemDisplayRules.ToArray();

            ItemDisplays.DisposeWhenDone();
        }

        /// <summary>
        /// Derived classes must implement this to populate itemDisplayRules.
        /// </summary>
        protected abstract void SetItemDisplayRules(List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules);
    }
}
