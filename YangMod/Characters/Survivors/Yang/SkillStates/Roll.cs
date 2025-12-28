// EntityStates/YangDodgeRoll.cs
using EntityStates;
using RoR2;
using UnityEngine;

namespace EntityStates
{
    public class YangDodgeRoll : BaseState
    {
        public static float baseDuration = 0.5f;
        public static float rollSpeed = 18f;
        private float stopwatch;

        public override void OnEnter()
        {
            base.OnEnter();
            stopwatch = 0f;

            PlayAnimation("FullBody, Override", "DodgeRoll", "Dodge.playbackRate", baseDuration);

            // Optionally disable hurtboxes or add a quick buff that grants invuln:
            // characterBody.AddTimedBuff(RoR2Content.Buffs.Cloak, baseDuration * 0.5f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            stopwatch += Time.fixedDeltaTime;

            Vector3 direction = base.inputBank?.aimDirection ?? characterBody.transform.forward;
            if (direction == Vector3.zero) direction = characterBody.transform.forward;
            if (characterMotor != null)
            {
                characterMotor.rootMotion += direction.normalized * rollSpeed * Time.fixedDeltaTime;
            }

            if (stopwatch >= baseDuration) outer.SetNextStateToMain();
        }

        public override InterruptPriority GetMinimumInterruptPriority() => InterruptPriority.Skill;
    }
}
