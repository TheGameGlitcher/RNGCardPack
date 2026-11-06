using LBoL.Base;
using LBoL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Structs
{
    public struct DamageRangeInfo
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public DamageType Type { get; set; }

        public bool IsAccuracy { get; set; }

        public readonly int Median => (int)Math.Round((MinDamage + MaxDamage) / 2.0, 0, MidpointRounding.AwayFromZero);

        public DamageRangeInfo(int minDamage, int maxDamage, DamageType type, bool isAccuracy)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Type = type;
            IsAccuracy = isAccuracy;
        }

        public readonly DamageRangeInfo Shift(int chaosOrder, bool allowNegative)
        {
            int finalMin = this.MinDamage - chaosOrder;
            int finalMax = this.MaxDamage + chaosOrder;

            if (!allowNegative && finalMin < 0)
                finalMin = 0;

            if (finalMin > this.Median)
                finalMin = this.Median;

            if (finalMax < this.Median)
                finalMax = this.Median;

            return new DamageRangeInfo(finalMin, finalMax, this.Type, this.IsAccuracy);
        }

        public override readonly string ToString()
        {
            return $"{MinDamage}~{MaxDamage}";
        }
    }
}
