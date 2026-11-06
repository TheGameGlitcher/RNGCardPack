using JetBrains.Annotations;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Utils;
using RNGCardPack.Source.Cards.Statuses;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class FragmentedGrimoireSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.LevelStackType = StackType.Max;

            config.ShowPlusByLimit = true;

            config.LimitStackType = StackType.Max;

            config.CountStackType = StackType.Max;

            config.HasCount = true;

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(FragmentedGrimoireSeDef))]
    public sealed class FragmentedGrimoireSe : RNGCardPackStatusEffect
    {
        protected override string GetBaseDescription()
        {
            if (Limit > 0)
                return ExtraDescription;

            else
                return BaseDescription;
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            Card[] cards = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Level, (CardConfig config) => config.Id != base.Id);
            List<Card> unknownCards = new List<Card>();

            foreach (Card card in cards)
            {
                Card unknownCard = Library.CreateCard(typeof(UnknownCard), false);

                CardConfig substituteConfig = unknownCard.Config.Copy();

                substituteConfig.Colors = card.Config.Colors;
                substituteConfig.Cost = card.Config.Cost;

                if (Limit > 0)
                    substituteConfig.Rarity = card.Config.Rarity;

                unknownCard.Config = substituteConfig;
                unknownCard.SetBaseCost(unknownCard.Config.Cost);

                unknownCards.Add(unknownCard);
            }

            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(unknownCards, false, false, false) { Source = this };

            yield return new InteractionAction(interaction, false);

            Card selectedCard = interaction.SelectedCard;

            Card finalCard = cards[unknownCards.IndexOf(selectedCard)];

            finalCard.SetTurnCost(Mana);
            finalCard.IsEthereal = true;
            finalCard.IsExile = true;

            if (selectedCard.IsUpgraded)
                yield return new UpgradeCardAction(finalCard);

            yield return new AddCardsToHandAction(new Card[] { finalCard });

            interaction = null;

            yield break;
        }
    }
}