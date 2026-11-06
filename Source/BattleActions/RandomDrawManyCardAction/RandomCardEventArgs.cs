using LBoL.Core;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomDrawManyCardAction
{
    public class RandomCardEventArgs : GameEventArgs
    {
        public Unit Source { get; set; }

        public int MinCards { get; set; }
        public int MaxCards { get; set; }
        public int MedianCards { get; set; }

        public bool AllowLuck { get; set; }

        public bool AllowOrderChaos { get; set; }

        public bool AllowNegativeValues { get; set; }

        public int Result { get; set; }

        public Dictionary<string, RollRandomRangeAction> RollActions;

        protected override string GetBaseDebugString()
        {
            return $"{GameEventArgs.DebugString(Source)} --- {{{MinCards}~{MaxCards}}}";
        }
    }
}
