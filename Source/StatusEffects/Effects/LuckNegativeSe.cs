using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
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
    public sealed class LuckNegativeSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Type = StatusEffectType.Negative;

            config.VFX = "DebuffGreen";

            return config;
        }
    }

    [EntityLogic(typeof(LuckNegativeSeDef))]
    public sealed class LuckNegativeSe : RNGCardPackStatusEffect, IOpposing<LuckSe>
    {
        public OpposeResult Oppose(LuckSe other)
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