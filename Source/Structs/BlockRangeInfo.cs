using LBoL.Core;
using LBoL.Core.Cards;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.Structs
{
    public struct BlockRangeInfo
    {
        public int MinBlock { get; set; }
        public int MaxBlock { get; set; }

        public readonly int Median => (int)Math.Round((MinBlock + MaxBlock) / 2.0, 0, MidpointRounding.AwayFromZero);

        public BlockShieldType Type { get; set; }

        public BlockRangeInfo(int minBlock, int maxBlock, BlockShieldType type)
        {
            MinBlock = minBlock;
            MaxBlock = maxBlock;
            Type = type;
        }

        public readonly BlockRangeInfo Shift(int chaosOrder, bool allowNegative)
        {
            int finalMin = this.MinBlock - chaosOrder;
            int finalMax = this.MaxBlock + chaosOrder;

            if (!allowNegative && finalMin < 0)
                finalMin = 0;

            if (finalMin > this.Median)
                finalMin = this.Median;

            if (finalMax < this.Median)
                finalMax = this.Median;

            return new BlockRangeInfo(finalMin, finalMax, this.Type);
        }

        public override readonly string ToString()
        {
            return $"{MinBlock}~{MaxBlock}";
        }
    }
}
