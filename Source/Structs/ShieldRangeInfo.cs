using LBoL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Structs
{
    public struct ShieldRangeInfo
    {
        public int MinShield { get; set; }
        public int MaxShield { get; set; }

        public readonly int Median => (int)Math.Round((MinShield + MaxShield) / 2.0, 0, MidpointRounding.AwayFromZero);

        public BlockShieldType Type { get; set; }

        public ShieldRangeInfo(int minShield, int maxShield, BlockShieldType type)
        {
            MinShield = minShield;
            MaxShield = maxShield;
            Type = type;
        }

        public readonly ShieldRangeInfo Shift(int chaosOrder, bool allowNegative)
        {
            int finalMin = this.MinShield - chaosOrder;
            int finalMax = this.MaxShield + chaosOrder;

            if (!allowNegative && finalMin < 0)
                finalMin = 0;

            if (finalMin > this.Median)
                finalMin = this.Median;

            if (finalMax < this.Median)
                finalMax = this.Median;

            return new ShieldRangeInfo(finalMin, finalMax, this.Type);
        }

        public override readonly string ToString()
        {
            return $"{MinShield}~{MaxShield}";
        }
    }
}
