using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class ReverseHierarchySeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            config.VFX = "BuffBlue";

            config.HasCount = true;

            config.CountStackType = StackType.Max;

            config.Keywords = Keyword.Upgrade;

            return config;
        }
    }

    [EntityLogic(typeof(ReverseHierarchySeDef))]
    public sealed class ReverseHierarchySe : RNGCardPackStatusEffect
    {
        public ReverseHierarchySe() : base()
        {
            PercentValue1 = 25;
        }

        protected override void OnAdded(Unit unit)
       {
            React(PerformAction.Effect(base.Battle.Player, "BuffRed"));

            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            else
            {
                base.ReactOwnerEvent<CardMovingEventArgs>(base.Battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(this.OnCardMoved));
                base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(this.OnAddCardToHand));
                base.HandleOwnerEvent<CardEventArgs>(base.Battle.CardDrawn, new GameEventHandler<CardEventArgs>(this.OnCardDrawn));
            }
       }

        private IEnumerable<BattleAction> OnCardMoved(CardMovingEventArgs args)
        {
            if (base.Battle.BattleShouldEnd || args.ActionSource == this)
                yield break;

            if (args.DestinationZone == CardZone.Hand)
            {
                yield return new RemoveCardAction(args.Card);

                yield return AddRandomCards();
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnAddCardToHand(CardsEventArgs args)
        {
            if (base.Battle.BattleShouldEnd || args.ActionSource == this)
                yield break;

            foreach (Card card in args.Cards)
            {
                yield return new RemoveCardAction(card);

                yield return AddRandomCards();
            }

            yield break;
        }

        private void OnCardDrawn(CardEventArgs args)
        {
            if (base.Battle.BattleShouldEnd || args.ActionSource == this)
                return;

            React(new RemoveCardAction(args.Card));

            React(AddRandomCards());

            return;
        }

        private BattleAction AddRandomCards()
        {
            Card[] cards = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), Level);

            foreach (Card card in cards)
            {
                if (RNGGenericPercentSe(PercentValue1, true))
                    card.Upgrade();

                card.SetBaseCost(ManaGroup.Anys(card.ConfigCost.Amount));
            }

            NotifyActivating();

            return new AddCardsToHandAction(cards);
        }
    }
}
