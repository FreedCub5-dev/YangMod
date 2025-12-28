using RoR2;
using RoR2.ContentManagement;
using System.Collections;
using System.Collections.Generic;

namespace YangMod.Modules
{
    public static class ContentPacks
    {
        internal static ContentPack contentPack;

        // Only add things here that you truly want in the ContentPack.
        internal static readonly List<SurvivorDef> survivorDefs = new List<SurvivorDef>();

        public static void Init()
        {
            contentPack = new ContentPack();
            ContentManager.collectContentPackProviders += AddContentPackProvider;
        }

        private static void AddContentPackProvider(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new Provider());
        }

        private sealed class Provider : IContentPackProvider
        {
            public string identifier => "YangMod.Content";

            public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
            {
                // Build your defs/prefabs here (safe time for defs; prefabs depend on what you instantiate).
                YangMod.Survivors.Yang.YangSurvivor.BuildContentOnce();

                if (survivorDefs.Count > 0)
                    contentPack.survivorDefs.Add(survivorDefs.ToArray());

                args.ReportProgress(1f);
                yield break;
            }

            public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
            {
                ContentPack.Copy(contentPack, args.output);
                yield break;
            }

            public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
            {
                yield break;
            }
        }
    }
}
