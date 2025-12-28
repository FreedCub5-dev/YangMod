// EntityStates/YangShotgun.cs
using EntityStates;
using RoR2;
using UnityEngine;
using YangMod.Modules;

namespace EntityStates
{
    public class YangShotgun : BaseState
    {
        public static int pelletCount = 8;
        public static float pelletDamage = 0.45f;
        public static float baseDuration = 0.5f;
        public static float spreadDegrees = 12f;

        private float stopwatch;

        public override void OnEnter()
        {
            base.OnEnter();
            stopwatch = 0f;

            PlayAnimation("Gesture, Override", "Shotgun", "Shotgun.playbackRate", baseDuration);

            if (isAuthority)
            {
                FireShotgun();
            }
        }

        private void FireShotgun()
        {
            Ray aimRay = GetAimRay();
            for (int i = 0; i < pelletCount; i++)
            {
                Vector3 dir = UtilHelpers.ApplySpread(aimRay.direction, spreadDegrees);
                BulletAttack ba = new BulletAttack
                {
                    owner = gameObject,
                    weapon = gameObject,
                    origin = aimRay.origin,
                    aimVector = dir,
                    minSpread = 0f,
                    maxSpread = 0f,
                    damage = base.damageStat * pelletDamage,
                    force = 120f,
                    tracerEffectPrefab = null,
                    muzzleName = "Muzzle", // replace if your model uses a different muzzle transform name
                    isCrit = base.RollCrit(),
                    falloffModel = BulletAttack.FalloffModel.None,
                    procCoefficient = 0.3f,
                    radius = 0.12f,
                    maxDistance = 18f,
                    hitMask = LayerIndex.CommonMasks.bullet
                };
                ba.Fire();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            stopwatch += Time.fixedDeltaTime;
            if (stopwatch >= baseDuration) outer.SetNextStateToMain();
        }
    }
}
