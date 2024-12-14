using HenryMod.Modules.BaseStates;
using RoR2.Skills;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using R2API;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class SlashComboTriple : BaseMeleeAttack, SteppedSkillDef.IStepSetter
    {
        public int swingIndex;

        //used by the steppedskilldef to increment your combo whenever this state is entered
        public void SetStep(int i)
        {
            swingIndex = i;
        }

        //check our swingIndex if we're at the last hit of the combo
        public bool isComboFinisher => swingIndex == 2; //first hit is 0, so the third hit is 2

        public override void OnEnter()
        {
            //check combo finisher to change the attack hitbox. you'll have to set it up on your body and register it in your survivor setup code
            hitBoxGroupName = isComboFinisher ? "SwordBigGroup" : "SwordGroup";

            damageType = DamageTypeCombo.GenericPrimary;
            // combo fnisher has more damage
            damageCoefficient = isComboFinisher ? Content.CharacterStaticValues.swordFinisherDamageCoefficient : Content.CharacterStaticValues.swordDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;

            // combo finisher lasts a little longer
            baseDuration = isComboFinisher ? 2 : 1f;

            attackStartTimeFraction = 0.2f;
            attackEndTimeFraction = 0.4f;

            earlyExitTimeFraction = 0.6f;

            //combo finisher has a bit meatier hitstop. you get the pattern by now
            hitStopDuration = isComboFinisher ? 0.1f : 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            //congratulations, you now are a master of the ternary (?) operator (or you will be in a second when you look it up right now)
            swingSoundString = isComboFinisher ? "HenrySwordSwingEpic" : "HenrySwordSwing";
            playbackRateParam = "Slash.playbackRate";
            muzzleString = GetComboMuzzle();
            swingEffectPrefab = Content.CharacterAssets.swordSwingEffect;
            hitEffectPrefab = Content.CharacterAssets.swordHitImpactEffect;

            impactSound = Content.CharacterAssets.swordHitSoundEvent.index;

            base.OnEnter();

            PlayAttackAnimation();
        }

        //nani? what is this? overlapAttack? (mouse over it)
        protected override void ModifyOverlapAttack(OverlapAttack overlapAttack)
        {
            base.ModifyOverlapAttack(overlapAttack);

            //third hit in the combo applies a dot
            if (isComboFinisher)
            {
                R2API.DamageAPI.AddModdedDamageType(overlapAttack, Content.CharacterDamageTypes.ComboFinisherDebuffDamage);
            }
        }

        private string GetComboMuzzle()
        {
            //spawn your swing effect at a different point based on your combo step.
            switch (swingIndex)
            {
                default:
                case 0:
                    return "SwingLeft";
                case 1:
                    return "SwingRight";
                case 2:
                    return "SwingCenter";
            }
        }

        protected override void PlayAttackAnimation()
        {
            //play a adifferent animation based on what step of the combo you are currently in.
            switch (swingIndex)
            {
                case 0:
                    PlayCrossfade("Gesture, Override", "Slash1", playbackRateParam, duration, 0.1f * duration);
                    break;
                case 1:
                    PlayCrossfade("Gesture, Override", "Slash2", playbackRateParam, duration, 0.1f * duration);
                    break;
                case 2:
                    PlayCrossfade("Gesture, Override", "SlashFinisher", playbackRateParam, duration, 0.1f * duration);
                    break;
            }
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        //add these functions for steppedskilldefs
        //bit advanced so don't worry about this, it's for networking.
        //long story short this syncs a value from authority (current player) to all other clients, so the swingIndex is the same for all machines
        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(swingIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            swingIndex = reader.ReadInt32();
        }
    }
}