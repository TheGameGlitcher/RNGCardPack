using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.StatusEffects.Template;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class LuckSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            config.VFX = "BuffGreen";

            return config;
        }
    }

    [EntityLogic(typeof(LuckSeDef))]
    public sealed class LuckSe : RNGCardPackStatusEffect, IOpposing<LuckNegativeSe>
    {
        public OpposeResult Oppose(LuckNegativeSe other)
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
                    if (currentArgs.Result < args.Median)
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
                    if (!currentArgs.Result)
                    {
                        var newRoll = new RollPercentAction(currentArgs.Source, currentArgs.Value, false, true);

                        yield return newRoll;

                        currentArgs = newRoll.Args;
                    }

                    else
                    {
                        args.Result = true;

                        break;
                    }
                }
            }

            yield break;
        }
    }
}