using YangMod.Modules.BaseContent.Characters;

namespace YangMod.Modules.BaseContent.Characters
{
    public abstract class SurvivorBase<T> : CharacterBase<T>
        where T : SurvivorBase<T>, new()
    {
        // Keep empty unless you truly need extra shared survivor logic.
    }
}
