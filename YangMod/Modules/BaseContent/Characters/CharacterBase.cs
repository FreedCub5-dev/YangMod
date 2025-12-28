using UnityEngine;

namespace YangMod.Modules.BaseContent.Characters
{
    public struct BodyInfo
    {
        public string bodyName;
        public string bodyNameToken;
        public string subtitleNameToken;
        public GameObject bodyPrefab;
        public GameObject displayPrefab;
        public GameObject masterPrefab;
    }

    public abstract class CharacterBase<T> where T : CharacterBase<T>
    {
        public abstract string survivorName { get; }
        public abstract string survivorTokenPrefix { get; }

        public abstract GameObject bodyPrefab { get; protected set; }
        public abstract GameObject masterPrefab { get; protected set; }
        public abstract GameObject displayPrefab { get; protected set; }

        public virtual void Initialize() { }
    }
}
