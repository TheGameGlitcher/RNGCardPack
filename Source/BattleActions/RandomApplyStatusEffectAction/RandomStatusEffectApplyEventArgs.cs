using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Basic;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomApplyStatusEffectAction
{
    public class RandomStatusEffectApplyEventArgs : GameEventArgs
    {
        public Type Type { get; set; }

        public Unit Target { get; set; }

        public int? MinLevel { get; set; }
        public int? MaxLevel { get; set; }
        public int? MedianLevel { get; set; }
        public bool HasLevel { get; set; }

        public int? MinDuration { get; set; }
        public int? MaxDuration { get; set; }
        public int? MedianDuration { get; set; }
        public bool HasDuration { get; set; }

        public int? MinCount { get; set; }
        public int? MaxCount { get; set; }
        public int? MedianCount { get; set; }
        public bool HasCount { get; set; }

        public int? MinLimit { get; set; }
        public int? MaxLimit { get; set; }
        public int? MedianLimit { get; set; }
        public bool HasLimit { get; set; }

        public bool AutoDecreasing { get; set; }

        public float WaitTime { get; set; }

        public StatusEffectAddResult? AddResult { get; set; }

        public bool AllowLuck { get; set; }

        public bool AllowOrderChaos { get; set; }

        public bool AllowNegativeValues { get; set; }

        public int? LevelResult { get; set; }
        public int? DurationResult { get; set; }
        public int? LimitResult { get; set; }
        public int? CountResult { get; set; }

        public Dictionary<string, RollRandomRangeAction> RollActions;

        protected override string GetBaseDebugString()
        {
            StatusEffect effect = Library.CreateStatusEffect(Type);
            string value = effect.Name ?? effect.Id;
            StringBuilder stringBuilder = new StringBuilder().Append('{').Append(value);

            if (effect != null)
            {
                if (HasLevel)
                    stringBuilder.Append(" L: ").Append($"{MinLevel}~{MaxLevel}");

                if (HasDuration)
                    stringBuilder.Append(" D: ").Append($"{MinDuration}~{MaxDuration}");

                if (HasCount)
                    stringBuilder.Append(" C: ").Append($"{MinCount}~{MaxCount}").Append("/").Append($"{MinLimit}~{MaxLimit}");

                stringBuilder.Append('}');
            }

            else
                throw new ArgumentNullException(nameof(Type), "StatusEffect type cannot be null!");

            if (AddResult.HasValue)
            {
                StatusEffectAddResult valueOrDefault = AddResult.GetValueOrDefault();

                return $"{stringBuilder} -> {GameEventArgs.DebugString(Target)} (result: {valueOrDefault})";
            }

            return stringBuilder.ToString() + " -> " + GameEventArgs.DebugString(Target);
        }
    }
}
