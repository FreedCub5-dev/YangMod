// EntityStates/YangPunch2.cs
using EntityStates;
using RoR2;
using UnityEngine;
using YangMod.Modules;

namespace EntityStates
{
    public class YangPunch2 : BaseState
    {
        public static float damageCoefficient = 1.4f;
        public static float baseDuration = 0.45f;
        private float stopwatch;
        private bool fired;

        public override void OnEnter()
        {
            base.OnEnter();
            stopwatch = 0f;
            fired = false;

            PlayAnimation("FullBody, Override", "Punch2", "Punch.playbackRate", baseDuration);

            // small forward root motion - safer to use characterMotor for a quick impulse
            if (characterMotor) characterMotor.velocity += characterBody.transform.forward * 2.5f;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            stopwatch += Time.fixedDeltaTime;

            if (!fired && stopwatch >= 0.16f)
            {
                FireHit();
                fired = true;
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

            var hitboxGroup = UtilHelpers.GetHitboxGroup(body, "hand");
            var attack = new OverlapAttack
            {
                attacker = gameObject,
                inflictor = gameObject,
                damage = base.damageStat * damageCoefficient,
                damageType = DamageType.Generic,
                procCoefficient = 1f,
                hitBoxGroup = hitboxGroup,
                teamIndex = teamComponent.teamIndex,
                pushAwayForce = 300f
            };
            attack.Fire();
        }

        public override InterruptPriority GetMinimumInterruptPriority() => InterruptPriority.PrioritySkill;
    }
}
