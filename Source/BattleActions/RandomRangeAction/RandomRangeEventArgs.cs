using LBoL.Core;
using LBoL.Core.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomRangeAction
{
    public class RandomRangeEventArgs : GameEventArgs
    {
        public GameEntity Source { get; set; }

        public int Min { get; set; }
        public int Max { get; set; }
        public int Median { get; set; }

        public bool AllowLuck { get; set; }

        public bool AllowOrderChaos { get; set; }

        public bool AllowNegativeValues { get; set; }

        public bool IsReroll { get; set; }

        public int Result { get; set; }

        protected override string GetBaseDebugString()
        {
            return ((GameEventArgs.DebugString(Source) == "<null>" ? "No Source" : GameEventArgs.DebugString(Source)) + ($" --- {{{Min}~{Max}}} ") + ((IsReroll) ? "(Reroll)" : "(Natural Roll)"));
        }
    }
}
