using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.EntityLib.JadeBoxes;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.BRG
{
    public sealed class BlackMarketKaleidoscopeDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Red, ManaColor.Green, ManaColor.Black };

            config.Cost = new ManaGroup() { Red = 1, Green = 1, Black = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            config.Value2 = 5;

            config.Mana = new ManaGroup() { };

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.RelativeKeyword = Keyword.TempMorph | Keyword.Upgrade;
            config.UpgradedRelativeKeyword = Keyword.TempMorph | Keyword.Upgrade;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(BlackMarketKaleidoscopeDef))]
    public sealed class BlackMarketKaleidoscope : RNGCardPackCard
    {
        public BlackMarketKaleidoscope() : base()
        {
            IsRandom = true;

            BaseValue3 = 5;
            BaseUpgradedValue3 = 5;

            BaseValue4 = 5;
            BaseUpgradedValue4 = 5;

            BasePercentValue1 = 50;
            BaseUpgradedPercentValue1 = 50;

            BasePercentValue2 = 10;
            BaseUpgradedPercentValue2 = 20;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            List<Card> cards = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Value2, null).ToList<Card>();
            cards.AddRange(base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Value2, null).ToList<Card>());
            cards.AddRange(base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Value2, null).ToList<Card>());

            if (cards.Count > 0)
            {
                foreach (Card card in cards)
                {
                    if (RNGGenericPercent(PercentValue1, true))
                        card.SetTurnCost(Mana);

                    if (RNGGenericPercent(PercentValue2, true))
                        card.Upgrade();
                }
            }

            SelectCardInteraction interaction = new SelectCardInteraction(Value1, Value1, cards, SelectedCardHandling.DoNothing) { Source = this };

            yield return new InteractionAction(interaction, false);

            List<Card> selectedCards = interaction.SelectedCards.ToList<Card>();

            yield return new AddCardsToHandAction(selectedCards);

            yield break;
        }
    }
}