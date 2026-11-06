using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction
{
    public class RandomBlockShieldEventArgs : GameEventArgs
    {
        public Unit Source { get; set; }
        public Unit Target { get; set; }

        public int MinBlock { get; set; }
        public int MaxBlock { get; set; }
        public int MedianBlock { get; set; }

        public int MinShield { get; set; }
        public int MaxShield { get; set; }
        public int MedianShield { get; set; }

        public bool AllowLuck { get; set; }

        public bool AllowOrderChaos { get; set; }

        public bool AllowNegativeValues { get; set; }

        public int ShieldResult { get; set; }

        public int BlockResult { get; set; }

        public bool HasBlock { get; set; }
        public bool HasShield { get; set; }

        public BlockShieldType Type { get; set; }

        public Dictionary<string, RollRandomRangeAction> RollActions;

        protected override string GetBaseDebugString()
        {
            if (Type != BlockShieldType.Unspecified)
            {
                return $"{GameEventArgs.DebugString(Source)} --- {{B: {MinBlock}~{MaxBlock}, S: {MinShield}~{MaxShield}, Type: {Type}}} --> {GameEventArgs.DebugString(Target)}";
            }

            return $"{GameEventArgs.DebugString(Source)} --- {{B: {MinBlock}~{MaxBlock}, S: {MinShield}~{MaxShield} --> {GameEventArgs.DebugString(Target)}";
        }
    }
}
