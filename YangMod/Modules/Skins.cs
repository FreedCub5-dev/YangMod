using RoR2;
using System;
using UnityEngine;

namespace YangMod.Modules
{
    internal static class Skins
    {
        internal static SkinDef CloneSkinDef(SkinDef src, string newName, string newNameToken)
        {
            if (!src) return null;

            // IMPORTANT: Instantiate() preserves internal fields like SkinDefParams in modern RoR2.
            var clone = UnityEngine.Object.Instantiate(src);
            clone.name = newName;
            clone.nameToken = newNameToken;
            return clone;
        }

        internal static CharacterModel.RendererInfo[] CloneRendererInfos(CharacterModel.RendererInfo[] src)
        {
            if (src == null) return Array.Empty<CharacterModel.RendererInfo>();
            var copy = new CharacterModel.RendererInfo[src.Length];
            Array.Copy(src, copy, src.Length);
            return copy;
        }

        internal static void RecolorRendererInfosMaterials(CharacterModel.RendererInfo[] infos, Color color, float emission = 2.5f)
        {
            if (infos == null) return;

            for (int i = 0; i < infos.Length; i++)
            {
                var srcMat = infos[i].defaultMaterial;
                if (!srcMat) continue;

                var mat = UnityEngine.Object.Instantiate(srcMat);

                if (mat.HasProperty("_Color"))
                    mat.SetColor("_Color", color);

                if (mat.HasProperty("_BaseColor"))
                    mat.SetColor("_BaseColor", color);

                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", color * emission);
                }

                infos[i].defaultMaterial = mat;
            }
        }
    }
}
