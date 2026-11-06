using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Utils;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.Patches;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class WhiteRabbitLuckSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(WhiteRabbitLuckSeDef))]
    public sealed class WhiteRabbitLuckSe : RNGCardPackStatusEffect
    {
        public WhiteRabbitLuckSe() : base()
        {
            PercentValue1 = 20;
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            Card finalCard;

            List <Card> cardList = (from card in base.Battle.HandZone.Concat(base.Battle.DrawZoneToShow).Concat(base.Battle.DiscardZone)
                                   select card).ToList<Card>();

            for (int i = 0; i < Level; i++)
            {
                if (base.Battle.BattleShouldEnd)
                    yield break;

                var action = new RollPercentAction(base.Battle.Player, PercentValue1, true);

                yield return action;

                if (action.Args.Result)
                    finalCard = base.Battle.ExileZone.Sample(base.Battle.GameRun.BattleRng);

                else
                    finalCard = cardList.Sample(base.GameRun.BattleRng);

                if (finalCard != null)
                {
                    switch (finalCard.Zone)
                    {
                        case CardZone.Hand:
                            this.NotifyActivating();

                            UiUtils.UpdateHandCardsPannel(finalCard);

                            yield return new PlayCardAction(finalCard);
                            break;

                        case CardZone.Draw:
                        case CardZone.Discard:
                            this.NotifyActivating();

                            yield return new PlayCardAction(finalCard);
                            break;

                        case CardZone.Exile:
                            this.NotifyActivating();

                            Card copyCard = finalCard.CloneBattleCard();

                            copyCard.IsPlayTwiceToken = true;
                            copyCard.Zone = CardZone.None;

                            base.Battle.AddCardToFollowArea(copyCard);

                            yield return new PlayCardAction(copyCard);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(finalCard), "Target card is in an invalid zone.");
                    }
                }
            }

            yield break;
        }
    }

}