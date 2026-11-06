using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.StatusEffects.Template;
using System.Collections.Generic;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class TempLuckNegativeSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Type = StatusEffectType.Negative;

            config.RelativeEffects = new List<string> { "LuckNegativeSe" };

            config.VFX = "DebuffGreen";

            return config;
        }
    }

    [EntityLogic(typeof(TempLuckNegativeSeDef))]
    public sealed class TempLuckNegativeSe : RNGCardPackStatusEffect, IOpposing<TempLuckSe>
    {
        public OpposeResult Oppose(TempLuckSe other)
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

            ReactOwnerEvent(RNGCardEventsManager.RangeRolled, OnRangeRolled);
            ReactOwnerEvent(RNGCardEventsManager.PercentRolled, OnPercentRolled);
        }

        private IEnumerable<BattleAction> OnRangeRolled(RandomRangeEventArgs args)
        {
            if (args.AllowLuck && !args.IsReroll)
            {
                var currentArgs = args;

                for (int i = 0; i < Level; i++)
                {
                    if (currentArgs.Result >= args.Median)
                    {
                        var newRoll = new RollRandomRangeAction(currentArgs.Source, currentArgs.Min, currentArgs.Max, false, false, currentArgs.AllowNegativeValues, true);

                        yield return newRoll;

                        currentArgs = newRoll.Args;
                    }

                    else
                    {
                        args.Result = currentArgs.Result;

                        break;
                    }
                }
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnPercentRolled(PercentEventArgs args)
        {
            if (args.AllowLuck && !args.IsReroll)
            {
                var currentArgs = args;

                for (int i = 0; i < Level; i++)
                {
                    if (currentArgs.Result)
                    {
                        var newRoll = new RollPercentAction(currentArgs.Source, currentArgs.Value, false, true);

                        yield return newRoll;

                        currentArgs = newRoll.Args;
                    }

                    else
                    {
                        args.Result = false;

                        break;
                    }
                }
            }

            yield break;
        }
    }
}