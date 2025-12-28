using System;
using System.Reflection;
using RoR2;
using UnityEngine;

namespace YangMod.Modules
{
    /// <summary>
    /// Lightweight prefab helpers used by this project.
    /// These are intentionally minimal so they survive RoR2 / DLC updates.
    /// </summary>
    internal static class Prefabs
    {
        /// <summary>
        /// Creates a minimal CharacterBody prefab (enough for content registration and basic spawning).
        /// You will still want to replace/extend this with a proper cloned body later.
        /// </summary>
        internal static GameObject CreateBodyPrefab(string bodyName)
        {
            if (string.IsNullOrWhiteSpace(bodyName)) bodyName = "UnnamedBody";

            var bodyPrefab = new GameObject(bodyName);

            // Core RoR2 pieces
            var body = bodyPrefab.AddComponent<CharacterBody>();
            body.baseNameToken = bodyName.ToUpperInvariant() + "_BODY_NAME";

            bodyPrefab.AddComponent<HealthComponent>();
            bodyPrefab.AddComponent<TeamComponent>();
            bodyPrefab.AddComponent<InputBankTest>();
            bodyPrefab.AddComponent<CharacterDirection>();
            bodyPrefab.AddComponent<Interactor>();
            bodyPrefab.AddComponent<SkillLocator>();

            // Motor is optional for menu registration, but helps if you actually spawn the body.
            bodyPrefab.AddComponent<CharacterMotor>();

            // ModelLocator is required by a lot of code paths.
            bodyPrefab.AddComponent<ModelLocator>();

            return bodyPrefab;
        }

        /// <summary>
        /// Creates a simple display prefab (used by SurvivorDef.displayPrefab).
        /// </summary>
        internal static GameObject CreateDisplayPrefab(GameObject bodyPrefab)
        {
            var name = (bodyPrefab ? bodyPrefab.name : "Unknown") + "Display";
            var display = new GameObject(name);

            // Survivor select UI expects a ModelLocator chain.
            display.AddComponent<ModelLocator>();

            // If you have a model prefab, you can parent/assign it here later.
            return display;
        }

        /// <summary>
        /// Creates a blank master prefab and (best-effort) assigns its bodyPrefab reference.
        /// </summary>
        internal static GameObject CreateBlankMasterPrefab(string masterName, GameObject bodyPrefab)
        {
            if (string.IsNullOrWhiteSpace(masterName)) masterName = "UnnamedMaster";

            var masterGO = new GameObject(masterName);
            var master = masterGO.AddComponent<CharacterMaster>();

            // RoR2 has had field/name churn here across versions; set via reflection.
            TrySetMember(master, "bodyPrefab", bodyPrefab);
            TrySetMember(master, "bodyPrefabObject", bodyPrefab);

            return masterGO;
        }

        private static void TrySetMember(object instance, string memberName, object value)
        {
            if (instance == null) return;

            var t = instance.GetType();
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            // Field
            var f = t.GetField(memberName, flags);
            if (f != null && f.FieldType.IsInstanceOfType(value))
            {
                f.SetValue(instance, value);
                return;
            }

            // Property
            var p = t.GetProperty(memberName, flags);
            if (p != null && p.CanWrite && p.PropertyType.IsInstanceOfType(value))
            {
                p.SetValue(instance, value, null);
            }
        }
    }
}
