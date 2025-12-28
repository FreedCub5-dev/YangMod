// Modules/UtilHelpers.cs
using System.Linq;
using UnityEngine;
using RoR2;

namespace YangMod.Modules
{
    public static class UtilHelpers
    {
        /// <summary>
        /// Safely get the model transform for a characterbody-mounted model.
        /// </summary>
        public static Transform GetModelTransform(CharacterBody body)
        {
            if (body == null) return null;
            var modelTransform = body.modelLocator?.modelTransform;
            if (modelTransform == null)
            {
                var bodyObj = body.gameObject;
                var model = bodyObj.GetComponentInChildren<ModelLocator>();
                return model?.modelTransform;
            }
            return modelTransform;
        }

        /// <summary>
        /// Find a HitBoxGroup by (partial) name on the character model.
        /// </summary>
        public static HitBoxGroup GetHitboxGroup(CharacterBody body, string partialName)
        {
            var modelTransform = GetModelTransform(body);
            if (modelTransform == null) return null;
            var groups = modelTransform.GetComponentsInChildren<HitBoxGroup>();
            if (groups == null || groups.Length == 0) return null;
            // match by name contains (case-insensitive)
            return groups.FirstOrDefault(g => g.groupName != null && g.groupName.ToLower().Contains(partialName.ToLower()));
        }

        /// <summary>
        /// Wrapper for Util.ApplySpread: returns a direction vector for a given spread in degrees.
        /// This matches the common RoR2 overload expectations.
        /// </summary>
        public static Vector3 ApplySpread(Vector3 direction, float spreadDegrees)
        {
            float spreadRad = spreadDegrees * Mathf.Deg2Rad;
            // call the RoR2 Util.ApplySpread overload with the parameters used commonly:
            // Util.ApplySpread(Vector3, float minSpread, float maxSpread, float minYaw, float maxYaw, float spreadPitchScale)
            // Not all versions share exactly same overloads; this is the most compatible call.
            return Util.ApplySpread(direction, 0f, spreadRad, 0f, spreadRad, 0f);
        }
    }
}
