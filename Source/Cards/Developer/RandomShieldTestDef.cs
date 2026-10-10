using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Developer
{
    public sealed class RandomShieldTestDef : RNGCardPackCardTemplate
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

            config.Shield = 0;

            config.Type = CardType.Defense;

            config.TargetType = TargetType.Self;

            config.Keywords = Keyword.Shield;
            config.UpgradedKeywords = Keyword.Shield;

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(RandomShieldTestDef))]
    public sealed class RandomShieldTest : RNGCardPackCard
    {

        public RandomShieldTest() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinShield = 3;
            BaseUpgradedRandomMinShield = 5;

            BaseRandomMaxShield = 8;
            BaseUpgradedRandomMaxShield = 10;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return RNGBlockShieldAction(RandomMinShield, RandomMaxShield, true);
        }
    }
}