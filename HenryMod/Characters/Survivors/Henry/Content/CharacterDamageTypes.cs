using R2API;

namespace HenryMod.Survivors.Henry.Content
{
    public class CharacterDamageTypes
    {
        public static DamageAPI.ModdedDamageType ComboFinisherDebuffDamage;

        public static void Init()
        {
            ComboFinisherDebuffDamage = DamageAPI.ReserveDamageType();
        }
    }
}
