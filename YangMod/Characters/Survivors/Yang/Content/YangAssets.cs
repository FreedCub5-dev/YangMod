using UnityEngine;

namespace YangMod.Modules
{
    public static class YangAssets
    {
        public static AssetBundle MainAssetBundle;

        // Called without parameters
        public static void Init() { }

        // Called with loaded bundle
        public static void Init(AssetBundle bundle)
        {
            MainAssetBundle = bundle;
        }

        public static Sprite LoadSprite(string name)
        {
            if (!MainAssetBundle)
                return null;

            Sprite s = MainAssetBundle.LoadAsset<Sprite>(name);
            return s;
        }
    }
}
