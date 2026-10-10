using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Colors.B;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class SpacetimeSeamSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            config.HasCount = true;

            config.CountStackType = StackType.Max;

            return config;
        }
    }

    [EntityLogic(typeof(SpacetimeSeamSeDef))]
    public sealed class SpacetimeSeamSe : RNGCardPackStatusEffect
    {
        public SpacetimeSeamSe() : base()
        {
            PercentValue1 = 10;
        }

        public List<Card> StoredCards {  get; private set; } = new List<Card>();

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            else
            {
                base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));

                List<Card> allCards = (from card in base.Battle.DrawZone.Concat(base.Battle.HandZone).Concat(base.Battle.DiscardZone)
                                       select card).ToList<Card>();

                for (int i = 0; i < Level; i++)
                {
                    Card targetCard = allCards.Sample(base.GameRun.BattleCardRng);

                    React(new RemoveCardAction(targetCard));

                    if (Count == 10 && RNGGenericPercentSe(PercentValue1, true))
                        targetCard.Upgrade();

                    targetCard.IsPlayTwiceToken = true;

                    StoredCards.Add(targetCard);
                }
            }
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (Card card in StoredCards)
            {
                if (RNGGenericPercentSe(Count, true))
                {
                    NotifyActivating();

                    base.Battle.AddCardToFollowArea(card);

                    yield return new PlayCardAction(card);
                }
            }
        }
    }
}
