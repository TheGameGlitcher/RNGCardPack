using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomApplyStatusEffectAction;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.BattleActions.ApplyStatusEffectRangeAction
{
    public class RandomApplyStatusEffectAction : SimpleEventBattleAction<RandomStatusEffectApplyEventArgs>
    {
        public RandomApplyStatusEffectAction(Type type, Unit target, int? minLevel = null, int? maxLevel = null, int? minDuration = null, int? maxDuration = null, int? minCount = null, int? maxCount = null, int? minLimit = null, int? maxLimit = null, bool autoDecreasing = true, float waitTime = 0.2f, bool allowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false)
        {
            base.Args = new RandomStatusEffectApplyEventArgs
            {
                Type = type,
                Target = target,
                MinLevel = minLevel,
                MaxLevel = maxLevel,
                MinDuration = minDuration,
                MaxDuration = maxDuration,
                MinCount = minCount,
                MaxCount = maxCount,
                MinLimit = minLimit,
                MaxLimit = maxLimit,
                AutoDecreasing = autoDecreasing,
                WaitTime = waitTime,
                AllowLuck = allowLuck,
                AllowOrderChaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                RollActions = new Dictionary<string, RollRandomRangeAction>()
            };

            if (minLevel.HasValue && maxLevel.HasValue)
                base.Args.HasLevel = true;

            if (minDuration.HasValue && maxDuration.HasValue)
                base.Args.HasDuration = true;

            if (minLimit.HasValue && maxLimit.HasValue)
                base.Args.HasLimit = true;

            if (minCount.HasValue && maxCount.HasValue)
                base.Args.HasCount = true;
        }

        protected override void PreEventPhase()
        {
            Trigger(RNGCardEventsManager.StatusEffectRangeApplying);

            if (base.Args.HasLevel)
            {
                if (!base.Args.AllowNegativeValues && base.Args.MinLevel < 0)
                    base.Args.MinLevel = 0;

                base.Args.MedianLevel = (int)Math.Round(((double)base.Args.MinLevel + (double)base.Args.MaxLevel) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinLevel > base.Args.MedianLevel)
                    base.Args.MinLevel = base.Args.MedianLevel;

                if (base.Args.MaxLevel < base.Args.MedianLevel)
                    base.Args.MaxLevel = base.Args.MedianLevel;
            }

            if (base.Args.HasDuration)
            {
                if (!base.Args.AllowNegativeValues && base.Args.MinDuration < 0)
                    base.Args.MinDuration = 0;

                base.Args.MedianDuration = (int)Math.Round(((double)base.Args.MinDuration + (double)base.Args.MaxDuration) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinDuration > base.Args.MedianDuration)
                    base.Args.MinDuration = base.Args.MedianDuration;

                if (base.Args.MaxDuration < base.Args.MedianDuration)
                    base.Args.MaxDuration = base.Args.MedianDuration;
            }

            if (base.Args.HasLimit)
            {
                if (!base.Args.AllowNegativeValues && base.Args.MinLimit < 0)
                    base.Args.MinLimit = 0;

                base.Args.MedianLimit = (int)Math.Round(((double)base.Args.MinLimit + (double)base.Args.MaxLimit) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinLimit > base.Args.MedianLimit)
                    base.Args.MinLimit = base.Args.MedianLimit;

                if (base.Args.MaxLimit < base.Args.MedianLimit)
                    base.Args.MaxLimit = base.Args.MedianLimit;
            }

            if (base.Args.HasCount)
            {
                if (!base.Args.AllowNegativeValues && base.Args.MinCount < 0)
                    base.Args.MinCount = 0;

                base.Args.MedianCount = (int)Math.Round(((double)base.Args.MinCount + (double)base.Args.MaxCount) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinCount > base.Args.MedianCount)
                    base.Args.MinCount = base.Args.MedianCount;

                if (base.Args.MaxCount < base.Args.MedianCount)
                    base.Args.MaxCount = base.Args.MedianCount;
            }
        }

        protected override void MainPhase()
        {
            if (base.Args.HasLevel)
                base.Args.RollActions.Add(nameof(base.Args.LevelResult), new RollRandomRangeAction(null, (int)base.Args.MinLevel, (int)base.Args.MaxLevel, base.Args.AllowLuck, false, base.Args.AllowLuck));

            if (base.Args.HasDuration)
                base.Args.RollActions.Add(nameof(base.Args.DurationResult), new RollRandomRangeAction(null, (int)base.Args.MinDuration, (int)base.Args.MaxDuration, base.Args.AllowLuck, false, base.Args.AllowLuck));

            if (base.Args.HasLimit)
                base.Args.RollActions.Add(nameof(base.Args.LimitResult), new RollRandomRangeAction(null, (int)base.Args.MinLimit, (int)base.Args.MaxLimit, base.Args.AllowLuck, false, base.Args.AllowLuck));

            if (base.Args.HasCount)
                base.Args.RollActions.Add(nameof(base.Args.CountResult), new RollRandomRangeAction(null, (int)base.Args.MinCount, (int)base.Args.MaxCount, base.Args.AllowLuck, false, base.Args.AllowLuck));

            foreach (var element in base.Args.RollActions)
                React(element.Value);
        }

        protected override void PostEventPhase()
        {
            foreach (var element in base.Args.RollActions)
            {
                switch(element.Key)
                {
                    case nameof(base.Args.LevelResult):
                        base.Args.LevelResult = element.Value.Args.Result;
                        break;

                    case nameof(base.Args.DurationResult):
                        base.Args.DurationResult = element.Value.Args.Result;
                        break;

                    case nameof(base.Args.LimitResult):
                        base.Args.LimitResult = element.Value.Args.Result;
                        break;

                    case nameof(base.Args.CountResult):
                        base.Args.CountResult = element.Value.Args.Result;
                        break;

                    default:
                        throw new ArgumentException(nameof(element.Key), "Dictionary key did not match any existing result variables");
                }
            }

            if (base.Args.Type == typeof(Firepower))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(FirepowerNegative), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(FirepowerNegative))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(Firepower), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(Spirit))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(SpiritNegative), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(SpiritNegative))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(Spirit), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(LuckSe))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(LuckNegativeSe), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(LuckNegativeSe))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(LuckSe), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(ChaosSe))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(OrderSe), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else if (base.Args.Type == typeof(OrderSe))
            {
                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                else if (base.Args.LevelResult < 0)
                    React(new ApplyStatusEffectAction(typeof(ChaosSe), base.Args.Target, -base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));

                else
                    React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            else
            {
                if (base.Args.AllowNegativeValues)
                {
                    Debug.LogWarning($"{base.Args.Type.Name} is not supported for negative numbers. Values will be floored to 0.");

                    if (base.Args.HasLevel && !base.Args.AllowNegativeValues && base.Args.MinLevel < 0)
                        base.Args.MinLevel = 0;

                    if (base.Args.HasDuration && !base.Args.AllowNegativeValues && base.Args.MinDuration < 0)
                        base.Args.MinDuration = 0;

                    if (base.Args.HasLimit && !base.Args.AllowNegativeValues && base.Args.MinLimit < 0)
                        base.Args.MinLimit = 0;

                    if (base.Args.HasCount && !base.Args.AllowNegativeValues && base.Args.MinCount < 0)
                        base.Args.MinCount = 0;

                    base.Args.AllowNegativeValues = false;
                }

                if (base.Args.LevelResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                if (base.Args.DurationResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                if (base.Args.CountResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                if (base.Args.LimitResult == 0)
                    base.Args.ForceCancelBecause(CancelCause.Failure);

                React(new ApplyStatusEffectAction(base.Args.Type, base.Args.Target, base.Args.LevelResult, base.Args.DurationResult, base.Args.CountResult, base.Args.LimitResult, base.Args.WaitTime, base.Args.AutoDecreasing));
            }

            Trigger(RNGCardEventsManager.StatusEffectRangeApplied);
        }
    }
}
