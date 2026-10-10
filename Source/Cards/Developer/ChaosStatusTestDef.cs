using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Developer
{
    public sealed class ChaosStatusTestDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.DebugLevel = 3;

            config.HideMesuem = true;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.Cost = new ManaGroup() { Any = 1 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Self;

            config.RelativeEffects = new List<string>() { "ChaosSe" };
            config.UpgradedRelativeEffects = new List<string>() { "ChaosSe" };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(ChaosStatusTestDef))]
    public sealed class ChaosStatusTest : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return BuffAction<ChaosSe>(Value1);
        }
    }
}