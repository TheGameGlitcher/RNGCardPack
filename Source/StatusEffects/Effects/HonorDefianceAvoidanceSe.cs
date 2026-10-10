using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Base;
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
    public sealed class HonorDefianceAvoidanceSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasCount = true;

            config.CountStackType = StackType.Max;

            config.RelativeEffects = new List<string>() { "OrderSe", "ChaosSe"};

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(HonorDefianceAvoidanceSeDef))]
    public sealed class HonorDefianceAvoidanceSe : RNGCardPackStatusEffect
    {
        public List<Card> StoredCards { get; private set; } = new List<Card>();

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            else
            {
                base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
                base.ReactOwnerEvent<GameEventArgs>(base.Battle.BattleEnding, new EventSequencedReactor<GameEventArgs>(this.OnBattleEnding));
            }
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            base.NotifyActivating();

            yield return new ApplyStatusEffectAction<OrderSe>(this.Owner, Level);
        }

        private IEnumerable<BattleAction> OnBattleEnding(GameEventArgs args)
        {
            if (!base.Battle.Player.HasStatusEffect<ChaosSe>())
            {
                base.NotifyActivating();

                yield return new HealAction(base.Battle.Player, base.Battle.Player, this.Count, HealType.Normal, 0.2f);
            }

            yield break;
        }
    }
}
