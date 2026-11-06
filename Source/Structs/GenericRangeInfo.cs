using LBoL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Structs
{
    public struct GenericRangeInfo
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public readonly int Median => (int)Math.Round((Min + Max) / 2.0, 0, MidpointRounding.AwayFromZero);

        public GenericRangeInfo(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public readonly GenericRangeInfo Shift(int chaosOrder, bool allowNegative)
        {
            int finalMin = this.Min - chaosOrder;
            int finalMax = this.Max + chaosOrder;

            if (!allowNegative && finalMin < 0)
                finalMin = 0;

            if (finalMin > this.Median)
                finalMin = this.Median;

            if (finalMax < this.Median)
                finalMax = this.Median;

            return new GenericRangeInfo(finalMin, finalMax);
        }

        public override readonly string ToString()
        {
            return $"{Min}~{Max}";
        }
    }
}