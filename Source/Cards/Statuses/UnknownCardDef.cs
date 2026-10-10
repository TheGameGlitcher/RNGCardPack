using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Statuses
{
    public sealed class UnknownCardDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.DebugLevel = 0;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.HideMesuem = true;

            config.Rarity = Rarity.Common;

            config.Type = CardType.Status;

            config.TargetType = TargetType.Nobody;

            config.Illustrator = "三折塔";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(UnknownCardDef))]
    public sealed class UnknownCard : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield break;
        }
    }
}