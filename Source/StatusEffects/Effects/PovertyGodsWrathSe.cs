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
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class PovertyGodsWrathSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasCount = true;

            config.CountStackType = StackType.Add;

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(PovertyGodsWrathSeDef))]
    public sealed class PovertyGodsWrathSe : RNGCardPackStatusEffect
    {
        public PovertyGodsWrathSe() : base()
        {
            PercentValue1 = 50;
        }

        private readonly List<RNGCardPackCard> cardEventSubscribers = new List<RNGCardPackCard>();
        private readonly List<RNGCardPackStatusEffect> statusEventSubscribers = new List<RNGCardPackStatusEffect>();

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));

            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));

            base.HandleOwnerEvent<CardTransformEventArgs>(base.Battle.CardTransformed, new GameEventHandler<CardTransformEventArgs>(this.OnCardTransformed));

            base.HandleOwnerEvent<StatusEffectApplyEventArgs>(base.Battle.Player.StatusEffectAdded, new GameEventHandler<StatusEffectApplyEventArgs>(this.OnAddStatusEffect));

            base.HandleOwnerEvent<StatusEffectEventArgs>(base.Battle.Player.StatusEffectRemoved, new GameEventHandler<StatusEffectEventArgs>(this.OnRemoveStatusEffect));

            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnTurnStarted));

            List<Card> cards = base.Battle.EnumerateAllCards().ToList();

            foreach (Card card in cards)
            {
                if (card is RNGCardPackCard && (card as RNGCardPackCard).IsRandom && !cardEventSubscribers.Contains(card))
                    cardEventSubscribers.Add(card as RNGCardPackCard);
            }

            List<StatusEffect> statuses = base.Battle.Player.StatusEffects.ToList();

            foreach (StatusEffect status in statuses)
            {
                if (status is RNGCardPackStatusEffect && !statusEventSubscribers.Contains(status))
                    statusEventSubscribers.Add(status as RNGCardPackStatusEffect);
            }

            foreach (RNGCardPackCard subscriber in cardEventSubscribers)
                subscriber.PercentRollFailed += OnPercentRollFailed;

            foreach (RNGCardPackStatusEffect subscriber in statusEventSubscribers)
                subscriber.PercentRollFailed += OnPercentRollFailed;
        }

        protected override void OnRemoved(Unit unit)
        {
            base.OnRemoved(unit);

            UnsubscribeCards(cardEventSubscribers.ToArray());

             foreach (RNGCardPackStatusEffect subscriber in statusEventSubscribers)
             {
                statusEventSubscribers.Remove(subscriber);
                subscriber.PercentRollFailed -= OnPercentRollFailed;
             }

        }

        private void OnAddCard(CardsEventArgs args)
        {
            SubscribeCards(args.Cards);
        }

        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            SubscribeCards(args.Cards);
        }

        private void OnCardTransformed(CardTransformEventArgs args)
        {
            if (args.SourceCard is RNGCardPackCard && (args.SourceCard as RNGCardPackCard).IsRandom && cardEventSubscribers.Contains(args.SourceCard as RNGCardPackCard))
            {
                cardEventSubscribers.Remove(args.SourceCard as RNGCardPackCard);
                (args.SourceCard as RNGCardPackCard).PercentRollFailed -= OnPercentRollFailed;
            }

            if (args.DestinationCard is RNGCardPackCard && (args.DestinationCard as RNGCardPackCard).IsRandom && !cardEventSubscribers.Contains(args.DestinationCard as RNGCardPackCard))
            {
                cardEventSubscribers.Add(args.DestinationCard as RNGCardPackCard);
                (args.DestinationCard as RNGCardPackCard).PercentRollFailed += OnPercentRollFailed;
            }
        }

        private void OnAddStatusEffect(StatusEffectApplyEventArgs args)
        {
            if (args.Effect is RNGCardPackStatusEffect && !statusEventSubscribers.Contains(args.Effect as RNGCardPackStatusEffect))
            {
                statusEventSubscribers.Add(args.Effect as RNGCardPackStatusEffect);
                (args.Effect as RNGCardPackStatusEffect).PercentRollFailed += OnPercentRollFailed;
            }
        }

        private void OnRemoveStatusEffect(StatusEffectEventArgs args)
        {
            if (args.Effect is RNGCardPackStatusEffect && statusEventSubscribers.Contains(args.Effect as RNGCardPackStatusEffect))
            {
                statusEventSubscribers.Remove(args.Effect as RNGCardPackStatusEffect);
                (args.Effect as RNGCardPackStatusEffect).PercentRollFailed -= OnPercentRollFailed;
            }
        }

        private IEnumerable<BattleAction> OnTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (Count > 0)
            {
                yield return new ApplyStatusEffectAction<Firepower>(base.Battle.Player, Count, null, null, null, 0.2f);

                this.NotifyActivating();
            }

            Count = 0;

            yield break;
        }

        private void SubscribeCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                if (card is RNGCardPackCard && (card as RNGCardPackCard).IsRandom && !cardEventSubscribers.Contains(card as RNGCardPackCard))
                {
                    cardEventSubscribers.Add(card as RNGCardPackCard);
                    (card as RNGCardPackCard).PercentRollFailed += OnPercentRollFailed;
                }
            }
        }

        private void UnsubscribeCards(Card[] cards)
        {
            foreach (Card card in cards)
            {
                if (card is RNGCardPackCard && (card as RNGCardPackCard).IsRandom && cardEventSubscribers.Contains(card))
                {
                    cardEventSubscribers.Remove(card as RNGCardPackCard);
                    (card as RNGCardPackCard).PercentRollFailed -= OnPercentRollFailed;
                }
            }
        }

        private void OnPercentRollFailed(object source, EventArgs args)
        {
            if (RNGGenericPercentSe(PercentValue1, false, false))
            {
                Count += Level;

                this.NotifyActivating();
            }
        }
    }
}