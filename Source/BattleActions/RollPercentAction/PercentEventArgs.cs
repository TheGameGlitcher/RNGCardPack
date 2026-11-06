using LBoL.Core;
using LBoL.Core.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RollPercentAction
{
    public class PercentEventArgs : GameEventArgs
    {
        public GameEntity Source { get; set; }

        public int Value { get; set; }

        public bool AllowLuck { get; set; }

        public bool IsReroll { get; set; }

        public bool Result { get; set; }

        protected override string GetBaseDebugString()
        {
            return $"{GameEventArgs.DebugString(Source)} --- {{{Value}%}} " + (IsReroll ? "(Reroll)" : "(Natural Roll)");
        }
    }
}
