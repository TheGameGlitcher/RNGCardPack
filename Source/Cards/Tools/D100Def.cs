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
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Tools
{
    public sealed class D100Def : RNGCardPackCardTemplate
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

            config.Value2 = 1;

            config.ToolPlayableTimes = 1;

            config.Type = CardType.Tool;

            config.TargetType = TargetType.Nobody;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe", "Firepower", "Spirit", "TempLuckSe", "TempChaosSe" };

            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe", "Firepower", "Spirit", "TempLuckSe", "TempChaosSe" };

            config.Illustrator = "TikaratheMew";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(D100Def))]
    public sealed class D100 : RNGCardPackCard
    {
        public D100() : base()
        {
            IsRandom = true;

            BaseValue3 = 2;
            BaseValue4 = 2;
            BaseValue5 = 5;
            BaseValue6 = 3;
            BaseValue7 = 6;
            BaseValue8 = 4;
            BaseValue9 = 8;

            BaseRandomMinValue1 = 1;
            BaseRandomMaxValue1 = 9;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            else
            {
                int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1);

                switch (result)
                {
                    case 1:
                        Card[] randomCardArray = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Value1, (CardConfig config) => config.Id != "D100");

                        foreach (Card randomCard in randomCardArray)
                        {
                            randomCard.IsPlayTwiceToken = true;
                            yield return new PlayCardAction(randomCard);
                        }
                        break;

                    case 2:
                        Card diceCard = Library.CreateCard(DiceCards[(RNGGenericRange(0, DiceCards.Count - 1))]);

                        diceCard.IsPlayTwiceToken = true;
                        yield return new PlayCardAction(diceCard);
                        break;

                    case 3:
                        yield return BuffAction<Firepower>(Value3);
                        break;

                    case 4:
                        yield return BuffAction<Spirit>(Value4);
                        break;

                    case 5:
                        yield return HealAction(Value5);
                        break;

                    case 6:
                        yield return BuffAction<LuckSe>(Value6);
                        break;

                    case 7:
                        yield return BuffAction<TempLuckSe>(Value7);
                        break;

                    case 8:
                        yield return BuffAction<ChaosSe>(Value8);
                        break;

                    case 9:
                        yield return BuffAction<TempChaosSe>(Value9);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(result), result, $"Result was set to an invalid value. Check the input variables for {nameof(RNGGenericRange)}.");
                }
            }
        }
    }
}
