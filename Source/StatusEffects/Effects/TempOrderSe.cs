using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomApplyStatusEffectAction;
using RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.BattleActions.RandomDrawManyCardAction;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.StatusEffects.Template;
using System.Collections.Generic;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class TempOrderSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            config.RelativeEffects = new List<string> { "OrderSe" };

            return config;
        }
    }

    [EntityLogic(typeof(TempOrderSeDef))]
    public sealed class TempOrderSe : RNGCardPackStatusEffect, IOpposing<TempChaosSe>
    {
        public OpposeResult Oppose(TempChaosSe other)
        {
            if (base.Level < other.Level)
            {
                other.Level -= base.Level;

                return OpposeResult.KeepOther;
            }

            if (base.Level == other.Level)
                return OpposeResult.Neutralize;

            base.Level -= other.Level;

            return OpposeResult.KeepSelf;
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.HandleOwnerEvent<UnitEventArgs>(unit.TurnEnded, delegate (UnitEventArgs _) { this.React(new RemoveStatusEffectAction(this, true, 0.1f)); });

            HandleOwnerEvent(RNGCardEventsManager.RangeRolling, OnGenericRangeRolling);
            HandleOwnerEvent(RNGCardEventsManager.BlockShieldRangeCasting, OnBlockShieldRangeCasting);
            HandleOwnerEvent(RNGCardEventsManager.DamageRangeDealing, OnDamageRangeDealing);
            HandleOwnerEvent(RNGCardEventsManager.CardRangeDrawing, OnCardRangeDrawing);
            HandleOwnerEvent(RNGCardEventsManager.StatusEffectRangeApplying, OnStatusEffectRangeApplying);
        }

        private void OnGenericRangeRolling(RandomRangeEventArgs args)
        {
            if (args.AllowOrderChaos)
            {
                args.Min += base.Level;
                args.Max -= base.Level;

                args.AddModifier(this);
            }
        }

        private void OnBlockShieldRangeCasting(RandomBlockShieldEventArgs args)
        {
            if (args.AllowOrderChaos)
            {
                if (args.HasBlock)
                {
                    args.MinBlock += base.Level;
                    args.MaxBlock -= base.Level;
                }

                if (args.HasShield)
                {
                    args.MinShield += base.Level;
                    args.MaxShield -= base.Level;
                }

                args.AddModifier(this);
            }
        }

        private void OnDamageRangeDealing(RandomDamageEventArgs args)
        {
            if (args.AllowOrderchaos)
            {
                args.MinDamage += base.Level;
                args.MaxDamage -= base.Level;

                args.AddModifier(this);
            }
        }

        private void OnCardRangeDrawing(RandomCardEventArgs args)
        {
            if (args.AllowOrderChaos)
            {
                args.MinCards += base.Level;
                args.MaxCards -= base.Level;

                args.AddModifier(this);
            }
        }

        private void OnStatusEffectRangeApplying(RandomStatusEffectApplyEventArgs args)
        {
            if (args.AllowOrderChaos)
            {
                if (args.HasCount)
                {
                    args.MinCount += base.Level;
                    args.MaxCount -= base.Level;
                }

                if (args.HasDuration)
                {
                    args.MinDuration += base.Level;
                    args.MaxDuration -= base.Level;
                }

                if (args.HasLevel)
                {
                    args.MinLevel += base.Level;
                    args.MaxLevel -= base.Level;
                }

                if (args.HasLimit)
                {
                    args.MinLimit += base.Level;
                    args.MaxLimit -= base.Level;
                }

                args.AddModifier(this);
            }
        }
    }
}