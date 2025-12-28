// BaseMasteryAchievement.cs
// Pared-down, compile-safe base mastery achievement for YangMod.
// Adjust RequiredCharacterBody / RequiredDifficultyCoefficient in your concrete achievement.

using RoR2;
using RoR2.Achievements;

namespace YangMod.Modules.Achievements
{
    public abstract class BaseMasteryAchievement : BaseAchievement
    {
        // Derived achievements must provide these.
        public abstract string RequiredCharacterBody { get; }
        public abstract float RequiredDifficultyCoefficient { get; }

        // Match BaseAchievement visibility: protected override
        protected override BodyIndex LookUpRequiredBodyIndex()
        {
            return BodyCatalog.FindBodyIndex(RequiredCharacterBody);
        }

        protected override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        protected override void OnBodyRequirementBroken()
        {
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding) return;

            if (!runReport.gameEnding.isWin) return;

            DifficultyIndex difficultyIndex = runReport.ruleBook.FindDifficulty();
            DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(difficultyIndex);
            if (difficultyDef == null) return;

            bool isDifficulty = difficultyDef.countsAsHardMode && difficultyDef.scalingValue >= RequiredDifficultyCoefficient;
            bool isInferno = difficultyDef.nameToken == "INFERNO_NAME";
            bool isEclipse = difficultyIndex >= DifficultyIndex.Eclipse1 && difficultyIndex <= DifficultyIndex.Eclipse8;

            if (isDifficulty || isInferno || isEclipse)
            {
                base.Grant();
            }
        }
    }
}
