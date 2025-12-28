using R2API;
using System.Collections.Generic;

namespace YangMod.Modules
{
    internal static class Language
    {
        internal static void Init()
        {
            Add("YANG_NAME", "Yang");
        }

        internal static void Add(string token, string text)
        {
            if (string.IsNullOrWhiteSpace(token)) return;

            LanguageAPI.Add(token, text);

            // FIX: was YangMod.Log.Debug(...)
            Log.Debug($"Token: {token} -> {text}");
        }

        internal static void Add(Dictionary<string, string> tokens)
        {
            if (tokens == null) return;
            foreach (var kv in tokens)
                Add(kv.Key, kv.Value);
        }
    }
}
