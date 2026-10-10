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
using LBoLEntitySideloader.Utils;
using RNGCardPack.Source.Cards.Statuses;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.G
{
    public sealed class AstrologicalDivinationDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Blue };

            config.IsXCost = true;

            config.Cost = new ManaGroup() { Blue = 3 };
            config.UpgradedCost = new ManaGroup() { Blue = 1 };

            config.Value1 = 1;

            config.Value2 = 5;

            config.Mana = new ManaGroup() { Any = 1 };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(AstrologicalDivinationDef))]
    public sealed class AstrologicalDivination : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            List<Card> cards = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Value2, (CardConfig config) => config.Id != base.Id).ToList<Card>();
            List<Card> unknownCards = new List<Card>();

            int excessMana = consumingMana.Amount - ((IsUpgraded) ? (int)Config.UpgradedCost?.Amount : Config.Cost.Amount);

            foreach (Card card in cards)
            {
                Card unknownCard = Library.CreateCard(typeof(UnknownCard), false);

                CardConfig substituteConfig = unknownCard.Config.Copy();

                switch(excessMana)
                {
                    case 0:
                        substituteConfig.Rarity = card.Config.Rarity;
                        break;

                    case 1:
                        substituteConfig.Rarity = card.Config.Rarity;
                        substituteConfig.Type = card.Config.Type;
                        break;

                    case 2:
                        ManaGroup hiddenGenericCost = new ManaGroup() { Any = card.Config.Cost.Amount };

                        substituteConfig.Rarity = card.Config.Rarity;
                        substituteConfig.Type = card.Config.Type;
                        unknownCard.IncreaseBaseCost(hiddenGenericCost);
                        break;

                    case 3:
                        substituteConfig.Rarity = card.Config.Rarity;
                        substituteConfig.Type = card.Config.Type;
                        substituteConfig.Colors = card.Config.Colors;
                        unknownCard.IncreaseBaseCost(card.Config.Cost);
                        break;
                }

                unknownCard.Config = substituteConfig;
                unknownCards.Add(unknownCard);
            }

            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction((excessMana > 3) ? cards : unknownCards, false, false, false) { Source = this };

            yield return new InteractionAction(interaction, false);

            Card selectedCard = interaction.SelectedCard;

            Card finalCard = (excessMana > 3) ? selectedCard : cards[unknownCards.IndexOf(selectedCard)];

            if (selectedCard.IsUpgraded)
                finalCard.Upgrade();

            finalCard.SetBaseCost(new ManaGroup() { Any = finalCard.Config.Cost.Amount });

            yield return new AddCardsToDeckAction(finalCard);

            Card deckCardByInstanceId = base.Battle.GameRun.GetDeckCardByInstanceId(finalCard.InstanceId);

            ManaGroup genericCost = new ManaGroup() { Any = finalCard.Config.Cost.Amount };
            ManaGroup? upgradedGenericCost;

            if (finalCard.Config.UpgradedCost != null)
                upgradedGenericCost = new ManaGroup() { Any = (int)finalCard.Config.UpgradedCost?.Amount };

            else
                upgradedGenericCost = null;

            deckCardByInstanceId.Config.Cost = genericCost;
            deckCardByInstanceId.Config.UpgradedCost = upgradedGenericCost;

            GenericCostCards.Add(deckCardByInstanceId.InstanceId);

            interaction = null;
        }

        public override IEnumerable<BattleAction> AfterUseAction()
        {
            Card deckCardByInstanceId = base.GameRun.GetDeckCardByInstanceId(base.InstanceId);

            if (deckCardByInstanceId != null)
                base.GameRun.RemoveDeckCard(deckCardByInstanceId, false);

            yield return new RemoveCardAction(this);

            yield break;
        }

        public override IEnumerable<BattleAction> AfterFollowPlayAction()
        {
            Card deckCardByInstanceId = base.GameRun.GetDeckCardByInstanceId(base.InstanceId);

            if (deckCardByInstanceId != null)
                base.GameRun.RemoveDeckCard(deckCardByInstanceId, false);

            yield return new RemoveCardAction(this);

            yield break;
        }
    }
}