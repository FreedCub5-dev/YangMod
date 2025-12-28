// YangModSupportStubs.cs
// Support stubs for ArcPh1lr3-style Henry tutorial compatibility.
// These are minimal placeholders to get the project compiling.
// Replace them with full implementations from the tutorial when ready.

using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using RoR2.Skills;
using EntityStates;

namespace YangMod
{
    // --- SkillDefInfo (public, full of fields used by tutorial code) ---
    public class SkillDefInfo
    {
        public string skillName;
        public string skillNameToken;
        public string skillDescriptionToken;
        public Sprite skillIcon;

        public EntityStates.SerializableEntityStateType activationState;
        public string activationStateMachineName = "Weapon";
        public EntityStates.InterruptPriority interruptPriority = EntityStates.InterruptPriority.Any;

        public float baseRechargeInterval = 0f;
        public int baseMaxStock = 1;
        public bool beginSkillCooldownOnSkillEnd = false;
        public bool mustKeyPress = false;
        public bool isCombatSkill = true;

        // Additional flags used by many templates
        public bool canceledFromSprinting = false;
        public bool cancelledFromSprinting = false;
        public bool cancelSprintingOnActivation = false;
        public bool dontAllowPastMaxStocks = false;
        public bool forceSprintDuringState = false;
        public bool resetCooldownTimerOnUse = false;
        public bool fullRestockOnAssign = false;
        public string[] keywordTokens = new string[0];
        public int requiredStock = 0;
        public int rechargeStock = 1;
        public float stockToConsume = 1f;

        public SkillDefInfo() { }

        public SkillDefInfo(string name, string nameToken, string descriptionToken, Sprite icon,
            EntityStates.SerializableEntityStateType state, string stateMachineName = "Weapon", bool mustKey = false)
        {
            skillName = name;
            skillNameToken = nameToken;
            skillDescriptionToken = descriptionToken;
            skillIcon = icon;
            activationState = state;
            activationStateMachineName = stateMachineName;
            mustKeyPress = mustKey;
        }
    }

    // --- Helper: store SkillFamily mapping for GenericSkill instances (safe stub) ---
    internal static class _SkillFamilyStore
    {
        private static readonly Dictionary<GenericSkill, SkillFamily> map = new Dictionary<GenericSkill, SkillFamily>();

        public static SkillFamily GetOrCreateFamily(GenericSkill key)
        {
            if (key == null) return null;
            if (!map.TryGetValue(key, out var family) || family == null)
            {
                family = ScriptableObject.CreateInstance<SkillFamily>();
                family.variants = new SkillFamily.Variant[0];
                map[key] = family;

                // Try assign to GenericSkill.skillFamily if writable (some RoR2 versions allow writing)
                try
                {
                    var prop = typeof(GenericSkill).GetProperty("skillFamily");
                    if (prop != null && prop.CanWrite) prop.SetValue(key, family);
                }
                catch { }
            }
            return map[key];
        }
    }

    // --- Skills helper stubs ---
    public static class Skills
    {
        public static void ClearGenericSkills(GameObject bodyPrefab)
        {
            if (bodyPrefab == null) return;
            foreach (var skill in bodyPrefab.GetComponentsInChildren<GenericSkill>())
            {
                UnityEngine.Object.DestroyImmediate(skill);
            }
        }

        public static SkillDef CreateSkillDef(SkillDefInfo info)
        {
            var skillDef = ScriptableObject.CreateInstance<SkillDef>();
            skillDef.skillName = info.skillName;
            skillDef.skillNameToken = info.skillNameToken;
            skillDef.skillDescriptionToken = info.skillDescriptionToken;
            skillDef.icon = info.skillIcon;
            skillDef.activationState = info.activationState;
            skillDef.activationStateMachineName = info.activationStateMachineName;
            skillDef.interruptPriority = info.interruptPriority;
            skillDef.baseRechargeInterval = info.baseRechargeInterval;
            skillDef.baseMaxStock = info.baseMaxStock;
            skillDef.mustKeyPress = info.mustKeyPress;
            skillDef.isCombatSkill = info.isCombatSkill;
            return skillDef;
        }

        public static T CreateSkillDef<T>(SkillDefInfo info) where T : SkillDef
        {
            T skillDef = ScriptableObject.CreateInstance<T>();
            skillDef.skillName = info.skillName;
            skillDef.skillNameToken = info.skillNameToken;
            skillDef.skillDescriptionToken = info.skillDescriptionToken;
            skillDef.icon = info.skillIcon;
            skillDef.activationState = info.activationState;
            skillDef.activationStateMachineName = info.activationStateMachineName;
            skillDef.interruptPriority = info.interruptPriority;
            skillDef.baseRechargeInterval = info.baseRechargeInterval;
            skillDef.baseMaxStock = info.baseMaxStock;
            skillDef.mustKeyPress = info.mustKeyPress;
            skillDef.isCombatSkill = info.isCombatSkill;
            return skillDef;
        }

        public static GenericSkill CreateGenericSkillWithSkillFamily(GameObject bodyPrefab, SkillSlot slot)
        {
            if (bodyPrefab == null) return null;
            var locator = bodyPrefab.GetComponent<SkillLocator>() ?? bodyPrefab.AddComponent<SkillLocator>();
            var genericSkill = bodyPrefab.AddComponent<GenericSkill>();
            _SkillFamilyStore.GetOrCreateFamily(genericSkill);
            return genericSkill;
        }

        // Overload used by some templates (string name). We just create a GenericSkill (slot unused in stub).
        public static GenericSkill CreateGenericSkillWithSkillFamily(GameObject bodyPrefab, string arbitraryName)
        {
            return CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Primary);
        }

        public static void AddSkillsToFamily(SkillFamily sf, SkillDef def)
        {
            if (sf == null || def == null) return;
            var list = new List<SkillFamily.Variant>(sf.variants ?? new SkillFamily.Variant[0]);
            list.Add(new SkillFamily.Variant { skillDef = def, unlockableDef = null, viewableNode = null });
            sf.variants = list.ToArray();
        }

        // No-op adders to avoid ambiguous overloads in the stub
        public static void AddPrimarySkills(GameObject bodyPrefab, SkillDef def) { }
        public static void AddSecondarySkills(GameObject bodyPrefab, SkillDef def) { }
        public static void AddUtilitySkills(GameObject bodyPrefab, SkillDef def) { }
        public static void AddSpecialSkills(GameObject bodyPrefab, SkillDef def) { }
    }

    // --- Minimal SurvivorBase<T> stub with virtuals required by YangSurvivor ---
    public abstract class SurvivorBase<T> where T : SurvivorBase<T>, new()
    {
        public virtual string assetBundleName => "";
        public virtual string bodyName => "";
        public virtual string masterName => "";
        public virtual string modelPrefabName => "";
        public virtual string displayPrefabName => "";
        public virtual string survivorTokenPrefix => "";

        public virtual BodyInfo bodyInfo { get; }
        public virtual UnlockableDef characterUnlockableDef { get; }
        public virtual CustomRendererInfo[] customRendererInfos { get; }
        public virtual ItemDisplaysBase itemDisplays { get; }

        public virtual AssetBundle assetBundle { get; protected set; }
        public virtual GameObject bodyPrefab { get; protected set; }
        public virtual CharacterBody prefabCharacterBody { get; protected set; }
        public virtual GameObject characterModelObject { get; protected set; }
        public virtual CharacterModel prefabCharacterModel { get; protected set; }
        public virtual GameObject displayPrefab { get; protected set; }

        public virtual void Initialize() { }
        public virtual void InitializeCharacter() { }
        public virtual void InitializeEntityStateMachines() { }
        public virtual void InitializeSkills() { }
        public virtual void InitializeSkins() { }
        public virtual void InitializeCharacterMaster() { }
    }

    // --- ItemDisplaysBase stub ---
    public abstract class ItemDisplaysBase
    {
        public virtual object CreateItemDisplays() { return null; }

        public virtual void SetItemDisplayRules(List<ItemDisplayRuleSet.KeyAssetRuleGroup> rules)
        {
            // no-op stub
        }
    }

    // --- Simple BodyInfo and renderer info stubs to satisfy overrides ---
    public class BodyInfo
    {
        public string bodyName;
        public string bodyNameToken;
        public string subtitleNameToken;
        public Texture characterPortrait;
        public Color bodyColor;
        public int sortPosition;
        public Sprite crosshair;
        public GameObject podPrefab;
        public float maxHealth;
        public float healthRegen;
        public float armor;
        public int jumpCount;
    }

    public class CustomRendererInfo
    {
        public string childName;
        public Material material;
    }

    // --- Log helper stub ---
    public static class Log
    {
        public static void Init() { }
        public static void Message(object o) => Debug.Log(o);
        public static void Warn(object o) => Debug.LogWarning(o);
        public static void Error(object o) => Debug.LogError(o);
    }

    // --- Buffs stub wrapper placeholder (actual registration uses R2API) ---
    public static class Buffs
    {
        public static BuffDef YangSemblanceBuff;
        // Keep RegisterBuffs in this stub for compatibility; actual file YangBuffs.cs will add the BuffAPI registration
        public static void RegisterBuffs() { }
    }

    // --- Tiny YangStates / YangTokens stubs ---
    public static class YangStates { public static void Init() { } }

    // --- Simple AI init stub ---
    public static class YangAI
    {
        public static void Init(GameObject bodyPrefab, string masterName) { Debug.Log("[YangMod] YangAI.Init (stub)"); }
    }

    // Minimal stub for BaseMasteryAchievement so the achievement compiles.
    // Remove this stub once you import the real tutorial achievement helpers.

    public abstract class BaseMasteryAchievement : RoR2.Achievements.BaseAchievement
    {
        // The achievement system expects these virtuals on mastery achievements.
        public virtual string RequiredCharacterBody => "";
        public virtual float RequiredDifficultyCoefficient => 1f;
    }

}
