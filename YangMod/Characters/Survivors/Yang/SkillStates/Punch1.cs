// EntityStates/YangPunch1.cs
using EntityStates;
using RoR2;
using UnityEngine;
using YangMod.Modules;

namespace EntityStates
{
    public class YangPunch1 : BaseState
    {
        public static float damageCoefficient = 1.0f;
        public static float baseDuration = 0.4f;
        private float stopwatch;
        private bool fired;

        public override void OnEnter()
        {
            base.OnEnter();
            stopwatch = 0f;
            fired = false;

            // Play the animation - replace layer/name with your animator states
            PlayAnimation("FullBody, Override", "Punch1", "Punch.playbackRate", baseDuration);

            // TODO: SFX/VFX
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            stopwatch += Time.fixedDeltaTime;

            if (!fired && stopwatch >= 0.13f)
            {
                FireHit();
                fired = true;
            }

            // chain into second punch if button pressed within window
            if (isAuthority && inputBank && inputBank.skill1.down && stopwatch >= 0.15f)
            {
                outer.SetNextState(new YangPunch2());
                return;
            }

            if (stopwatch >= baseDuration)
            {
                outer.SetNextStateToMain();
            }
        }

        private void FireHit()
        {
            if (!isAuthority) return;

            var body = characterBody;
            if (body == null) return;

            var hitboxGroup = UtilHelpers.GetHitboxGroup(body, "hand"); // uses partial name match; change if needed
            var attack = new OverlapAttack
            {
                attacker = gameObject,
                inflictor = gameObject,
                damage = base.damageStat * damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = DamageType.Generic,
                attackerFiltering = AttackerFiltering.Default,
                procCoefficient = 1f,
                hitBoxGroup = hitboxGroup,
                teamIndex = teamComponent.teamIndex,
                pushAwayForce = 200f
            };
            attack.Fire();
        }

        public override InterruptPriority GetMinimumInterruptPriority() => InterruptPriority.PrioritySkill;
    }
}
