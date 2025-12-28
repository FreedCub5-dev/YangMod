using EntityStates;

namespace YangMod.Modules
{
    // These classes redirect the template’s expected classes
    // to the REAL EntityStates from RoR2.
    public class GenericCharacterMainStub : GenericCharacterMain { }
    public class SpawnTeleporterStateStub : SpawnTeleporterState { }

    public enum InterruptPriorityStub
    {
        Any,
        Skill,
        Pain,
        Damage,
        Death
    }
}
