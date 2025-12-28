// Modules/Skills.cs
using EntityStates;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace YangMod.Modules
{
    internal static class Skills
    {
        #region SkillDefInfo helper

        internal struct SkillDefInfo
        {
            public string skillName;
            public string skillNameToken;
            public string skillDescriptionToken;
            public string[] keywordTokens;

            public Sprite skillIcon;

            public SerializableEntityStateType activationState;
            public string activationStateMachineName;
            public InterruptPriority interruptPriority;

            public bool isCombatSkill;
            public bool mustKeyPress;

            public float baseRechargeInterval;
            public int baseMaxStock;
            public int rechargeStock;
            public int requiredStock;
            public int stockToConsume;

            public bool resetCooldownTimerOnUse;
            public bool fullRestockOnAssign;
            public bool dontAllowPastMaxStocks;
            public bool beginSkillCooldownOnSkillEnd;

            public bool canceledFromSprinting;
            public bool cancelSprintingOnActivation;
            public bool forceSprintDuringState;

            public SkillDefInfo(
                string skillName,
                string skillNameToken,
                string skillDescriptionToken,
                Sprite icon,
                SerializableEntityStateType state,
                string stateMachineName,
                bool isCombatSkill = true)
            {
                this.skillName = skillName;
                this.skillNameToken = skillNameToken;
                this.skillDescriptionToken = skillDescriptionToken;
                this.keywordTokens = Array.Empty<string>();
                this.skillIcon = icon;

                this.activationState = state;
                this.activationStateMachineName = stateMachineName;
                this.interruptPriority = InterruptPriority.Skill;

                this.isCombatSkill = isCombatSkill;
                this.mustKeyPress = true;

                this.baseRechargeInterval = 0f;
                this.baseMaxStock = 1;
                this.rechargeStock = 1;
                this.requiredStock = 1;
                this.stockToConsume = 1;

                this.resetCooldownTimerOnUse = false;
                this.fullRestockOnAssign = true;
                this.dontAllowPastMaxStocks = false;
                this.beginSkillCooldownOnSkillEnd = false;

                this.canceledFromSprinting = false;
                this.cancelSprintingOnActivation = false;
                this.forceSprintDuringState = false;
            }
        }

        #endregion

        #region SkillDef creation

        internal static SkillDef CreateSkillDef(SkillDefInfo info)
        {
            var def = ScriptableObject.CreateInstance<SkillDef>();
            ApplyInfoToSkillDef(def, info);
            return def;
        }

        internal static T CreateSkillDef<T>(SkillDefInfo info) where T : SkillDef
        {
            var def = ScriptableObject.CreateInstance<T>();
            ApplyInfoToSkillDef(def, info);
            return def;
        }

        private static void ApplyInfoToSkillDef(SkillDef def, SkillDefInfo info)
        {
            def.skillName = info.skillName;
            def.skillNameToken = info.skillNameToken;
            def.skillDescriptionToken = info.skillDescriptionToken;
            def.icon = info.skillIcon;

            def.activationState = info.activationState;
            def.activationStateMachineName = info.activationStateMachineName;
            def.interruptPriority = info.interruptPriority;

            def.baseRechargeInterval = info.baseRechargeInterval;
            def.baseMaxStock = info.baseMaxStock;
            def.rechargeStock = info.rechargeStock;
            def.requiredStock = info.requiredStock;
            def.stockToConsume = info.stockToConsume;

            def.resetCooldownTimerOnUse = info.resetCooldownTimerOnUse;
            def.fullRestockOnAssign = info.fullRestockOnAssign;
            def.dontAllowPastMaxStocks = info.dontAllowPastMaxStocks;
            def.beginSkillCooldownOnSkillEnd = info.beginSkillCooldownOnSkillEnd;

            def.canceledFromSprinting = info.canceledFromSprinting;
            def.cancelSprintingOnActivation = info.cancelSprintingOnActivation;
            def.forceSprintDuringState = info.forceSprintDuringState;

            def.isCombatSkill = info.isCombatSkill;
            def.mustKeyPress = info.mustKeyPress;

            def.keywordTokens = info.keywordTokens ?? Array.Empty<string>();
        }

        #endregion

        #region GenericSkill helpers

        internal static void ClearGenericSkills(GameObject bodyPrefab)
        {
            if (!bodyPrefab) return;

            foreach (var gs in bodyPrefab.GetComponents<GenericSkill>())
            {
                UnityEngine.Object.DestroyImmediate(gs);
            }

            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (locator != null)
            {
                locator.primary = null;
                locator.secondary = null;
                locator.utility = null;
                locator.special = null;
            }
        }

        internal static GenericSkill CreateGenericSkillWithSkillFamily(GameObject bodyPrefab, SkillSlot slot)
        {
            var familyName = slot.ToString();
            return CreateGenericSkillWithSkillFamily(bodyPrefab, familyName, slot);
        }

        internal static GenericSkill CreateGenericSkillWithSkillFamily(GameObject bodyPrefab, string familyName, SkillSlot? slotOverride = null)
        {
            if (!bodyPrefab) throw new ArgumentNullException(nameof(bodyPrefab));

            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (!locator)
            {
                locator = bodyPrefab.AddComponent<SkillLocator>();
            }

            var genericSkill = bodyPrefab.AddComponent<GenericSkill>();
            genericSkill.skillName = familyName;

            var family = ScriptableObject.CreateInstance<SkillFamily>();
            family.variants = Array.Empty<SkillFamily.Variant>();

            // skillFamily is read-only – set the private backing field via reflection.
            var field = typeof(GenericSkill).GetField("_skillFamily",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (field != null)
            {
                field.SetValue(genericSkill, family);
            }

            var slot = slotOverride ?? SkillSlot.None;

            switch (slot)
            {
                case SkillSlot.Primary:
                    locator.primary = genericSkill;
                    break;
                case SkillSlot.Secondary:
                    locator.secondary = genericSkill;
                    break;
                case SkillSlot.Utility:
                    locator.utility = genericSkill;
                    break;
                case SkillSlot.Special:
                    locator.special = genericSkill;
                    break;
                default:
                    break;
            }

            return genericSkill;
        }

        #endregion

        #region Add skills to families

        internal static void AddSkillsToFamily(SkillFamily family, params SkillDef[] skillDefs)
        {
            if (family == null || skillDefs == null || skillDefs.Length == 0)
                return;

            var variants = new List<SkillFamily.Variant>();
            if (family.variants != null)
                variants.AddRange(family.variants);

            foreach (var def in skillDefs)
            {
                if (!def) continue;

                variants.Add(new SkillFamily.Variant
                {
                    skillDef = def,
                    unlockableDef = null,
                    viewableNode = new ViewablesCatalog.Node(def.skillNameToken ?? def.skillName, false, null)
                });
            }

            family.variants = variants.ToArray();
        }

        internal static void AddPrimarySkills(GameObject bodyPrefab, params SkillDef[] defs)
        {
            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (locator?.primary?.skillFamily != null)
            {
                AddSkillsToFamily(locator.primary.skillFamily, defs);
            }
        }

        internal static void AddSecondarySkills(GameObject bodyPrefab, params SkillDef[] defs)
        {
            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (locator?.secondary?.skillFamily != null)
            {
                AddSkillsToFamily(locator.secondary.skillFamily, defs);
            }
        }

        internal static void AddUtilitySkills(GameObject bodyPrefab, params SkillDef[] defs)
        {
            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (locator?.utility?.skillFamily != null)
            {
                AddSkillsToFamily(locator.utility.skillFamily, defs);
            }
        }

        internal static void AddSpecialSkills(GameObject bodyPrefab, params SkillDef[] defs)
        {
            var locator = bodyPrefab.GetComponent<SkillLocator>();
            if (locator?.special?.skillFamily != null)
            {
                AddSkillsToFamily(locator.special.skillFamily, defs);
            }
        }

        #endregion
    }
}
