using RoR2;
using System;
using UnityEngine;
using YangMod.Modules;
using YangMod.Modules.BaseContent.Characters;

namespace YangMod.Survivors.Yang
{
    public class YangSurvivor : SurvivorBase<YangSurvivor>
    {
        public override string survivorName => "Yang";
        public override string survivorTokenPrefix => "YANG";

        public override GameObject bodyPrefab { get; protected set; }
        public override GameObject masterPrefab { get; protected set; }
        public override GameObject displayPrefab { get; protected set; }

        public static SurvivorDef YangDef { get; private set; }

        private static bool _built;
        public static void BuildContentOnce()
        {
            if (_built) return;
            _built = true;

            new YangSurvivor().Initialize();
        }

        public override void Initialize()
        {
            // MODERN-SAFE: do NOT instantiate bodies/masters here.
            // Just reference vanilla so catalog/init never bricks.
            LoadVanillaBodyMaster_ReferenceOnly();

            // This is what survivor select uses for the mannequin.
            CreateDisplayPrefab_CloneCommandoDisplay();

            // Phase 4: add 2 skins to the DISPLAY prefab safely.
            SetupDisplaySkins_Phase4_MagentaDefault();

            RegisterSurvivorDef();

            YangPlugin.Log.LogInfo("[YangSurvivor] Built content (Phase 4: display clone + display skins; body/master reference-only).");
        }

        private void LoadVanillaBodyMaster_ReferenceOnly()
        {
            if (!bodyPrefab)
                bodyPrefab = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody");

            if (!bodyPrefab)
                YangPlugin.Log.LogError("[YangSurvivor] FAILED to load CommandoBody via Resources.Load");

            if (!masterPrefab)
                masterPrefab = Resources.Load<GameObject>("Prefabs/CharacterMasters/CommandoMaster");
            // masterPrefab is optional for survivor select; leave warning only.
            if (!masterPrefab)
                YangPlugin.Log.LogWarning("[YangSurvivor] CommandoMaster not found (ok for menu testing).");
        }

        private void CreateDisplayPrefab_CloneCommandoDisplay()
        {
            if (displayPrefab) return;

            var commandoDisplay = Resources.Load<GameObject>("Prefabs/CharacterDisplays/CommandoDisplay");
            if (!commandoDisplay)
            {
                YangPlugin.Log.LogError("[YangSurvivor] FAILED to load CommandoDisplay via Resources.Load");
                return;
            }

            displayPrefab = UnityEngine.Object.Instantiate(commandoDisplay);
            displayPrefab.name = "YangDisplay";

            YangPlugin.Log.LogInfo("[YangSurvivor] Display prefab cloned successfully (YangDisplay).");
        }

        private void RegisterSurvivorDef()
        {
            var def = ScriptableObject.CreateInstance<SurvivorDef>();
            def.cachedName = survivorName;
            def.displayNameToken = $"{survivorTokenPrefix}_NAME";
            def.descriptionToken = $"{survivorTokenPrefix}_DESCRIPTION";
            def.primaryColor = Color.magenta;

            def.bodyPrefab = bodyPrefab;
            def.displayPrefab = displayPrefab;
            def.unlockableDef = null;

            YangDef = def;
            ContentPacks.survivorDefs.Add(def);

            YangPlugin.Log.LogInfo("[YangSurvivor] Registered SurvivorDef for Yang.");
        }

        /// <summary>
        /// Puts 2 skins on the DISPLAY prefab.
        /// Index 0 = magenta (default)
        /// Index 1 = vanilla
        /// </summary>
        private void SetupDisplaySkins_Phase4_MagentaDefault()
        {
            if (!displayPrefab)
            {
                YangPlugin.Log.LogError("[YangSurvivor] SetupDisplaySkins: displayPrefab is null.");
                return;
            }

            var msc = displayPrefab.GetComponentInChildren<ModelSkinController>(true);
            if (!msc)
            {
                YangPlugin.Log.LogError("[YangSurvivor] SetupDisplaySkins: ModelSkinController not found on display prefab.");
                return;
            }

            if (msc.skins == null || msc.skins.Length == 0 || !msc.skins[0])
            {
                YangPlugin.Log.LogError("[YangSurvivor] SetupDisplaySkins: CommandoDisplay has no base skins to clone.");
                return;
            }

            // Base skin (vanilla) from CommandoDisplay (has SkinDefParams in modern builds)
            var baseSkin = msc.skins[0];

            // Clone vanilla as our vanilla (keeps params)
            var yangVanilla = Skins.CloneSkinDef(
                baseSkin,
                newName: "skinYangDisplayVanilla",
                newNameToken: $"{survivorTokenPrefix}_SKIN_DEFAULT_NAME"
            );

            // Clone again for magenta
            var yangMagenta = Skins.CloneSkinDef(
                baseSkin,
                newName: "skinYangDisplayMagenta",
                newNameToken: $"{survivorTokenPrefix}_SKIN_MAGENTA_NAME"
            );

            if (!yangVanilla || !yangMagenta)
            {
                YangPlugin.Log.LogError("[YangSurvivor] SetupDisplaySkins: Failed to clone SkinDef(s).");
                return;
            }

            // Ensure rendererInfos is a NEW array instance, then recolor materials for magenta.
            var magentaInfos = Skins.CloneRendererInfos(yangMagenta.rendererInfos);
            yangMagenta.rendererInfos = magentaInfos;
            Skins.RecolorRendererInfosMaterials(yangMagenta.rendererInfos, Color.magenta, emission: 2.5f);

            // Make magenta default (index 0)
            msc.skins = new SkinDef[]
            {
                yangMagenta,
                yangVanilla
            };

            YangPlugin.Log.LogInfo($"[YangSurvivor] Display skins installed: {msc.skins.Length} (magenta default index 0).");
        }
    }
}
