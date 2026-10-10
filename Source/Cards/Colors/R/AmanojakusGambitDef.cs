using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System.Collections.Generic;


namespace RNGCardPack.Source.Cards.Colors.R
{
    public sealed class AmanojakusGambitDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Red };

            config.Cost = new ManaGroup() { Red = 1, Any = 3 };
            config.UpgradedCost = new ManaGroup() { Red = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.RelativeKeyword = Keyword.Friend | Keyword.XCost;
            config.UpgradedRelativeKeyword = Keyword.Friend | Keyword.XCost;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(AmanojakusGambitDef))]
    public sealed class AmanojakusGambit : RNGCardPackCard
    {
        public AmanojakusGambit() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 4;
            BaseRandomMaxValue1 = 5;

            BaseUpgradedRandomMinValue1 = 4;
            BaseUpgradedRandomMaxValue1 = 5;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);
                
            Card[] cards = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), result, (CardConfig config) => config.Id != base.Id && !config.IsXCost && config.Type != CardType.Friend);

            foreach (Card card in cards)
            {
                if (base.Battle.UpgradeTempCardsLevel > 0)
                    card.Upgrade();

                card.IsPlayTwiceToken = true;
                base.Battle.AddCardToFollowArea(card);

                yield return new PlayCardAction(card);
            }

            yield break;
        }
    }
}