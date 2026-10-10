using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack;

namespace RNGCardPack.Source.Localization
{
    public sealed class RNGCardPackLocalization
    {
        public static string Cards = "Cards";
        public static string Exhibits = "Exhibits";
        public static string PlayerUnit = "PlayerUnit";
        public static string EnemiesUnit = "EnemyUnit";
        public static string UnitModel = "UnitModel";
        public static string UltimateSkills = "UltimateSkills";
        public static string StatusEffects = "StatusEffects";
        public static string JadeBoxes = "JadeBoxes";
        public static string Packs = "Packs";

        public static BatchLocalization CardsBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(CardTemplate), Cards);
        public static BatchLocalization ExhibitsBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(ExhibitTemplate), Exhibits);
        public static BatchLocalization PlayerUnitBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(PlayerUnitTemplate), PlayerUnit);
        public static BatchLocalization EnemiesUnitBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(EnemyUnitTemplate), EnemiesUnit);
        public static BatchLocalization UnitModelBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(UnitModelTemplate), UnitModel);
        public static BatchLocalization UltimateSkillsBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(UltimateSkillTemplate), UltimateSkills);
        public static BatchLocalization StatusEffectsBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(StatusEffectTemplate), StatusEffects);
        public static BatchLocalization JadeBoxBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(JadeBoxTemplate), JadeBoxes);
        public static BatchLocalization PacksBatchLoc = new BatchLocalization(BepinexPlugin.directorySource, typeof(PackTemplate), Packs);


        // maybe it's better to have controlled file discovery tah
        public static void Init()
        {
            CardsBatchLoc.DiscoverAndLoadLocFiles(Cards);
            ExhibitsBatchLoc.DiscoverAndLoadLocFiles(Exhibits);
            PlayerUnitBatchLoc.DiscoverAndLoadLocFiles(PlayerUnit);
            EnemiesUnitBatchLoc.DiscoverAndLoadLocFiles(EnemiesUnit);
            UnitModelBatchLoc.DiscoverAndLoadLocFiles(UnitModel);
            UltimateSkillsBatchLoc.DiscoverAndLoadLocFiles(UltimateSkills);
            StatusEffectsBatchLoc.DiscoverAndLoadLocFiles(StatusEffects);
            JadeBoxBatchLoc.DiscoverAndLoadLocFiles(JadeBoxes);
            PacksBatchLoc.DiscoverAndLoadLocFiles(Packs);
        }
    }
}