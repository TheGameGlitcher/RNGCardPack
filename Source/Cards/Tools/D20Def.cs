using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Tools
{
    public sealed class D20Def : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.Cost = new ManaGroup() { };

            config.IsUpgradable = false;

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;

            config.ToolPlayableTimes = 1;

            config.Type = CardType.Tool;

            config.TargetType = TargetType.Nobody;

            config.RelativeKeyword = Keyword.Upgrade;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "TikaratheMew";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(D20Def))]
    public sealed class D20 : RNGCardPackCard
    {
        public D20() : base()
        {
            IsRandom = true;

            BasePercentValue1 = 10;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            else
            {
                int handSize = base.Battle.HandZone.Count;
                Card[] array = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot, false), (handSize * Value1), null);

                if (handSize > 0)
                    yield return new ExileManyCardAction(base.Battle.HandZone);

                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        if (RNGGenericPercent(PercentValue1, true))
                            card.Upgrade();
                    }

                    yield return new AddCardsToHandAction(array);
                }
            }
        }
    }
}
