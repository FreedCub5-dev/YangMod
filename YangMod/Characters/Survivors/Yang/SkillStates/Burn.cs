using EntityStates;

namespace YangMod.Survivors.Yang.SkillStates
{
    public class Burn : EntityState
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= 0.5f)
                outer.SetNextStateToMain();
        }
    }
}
