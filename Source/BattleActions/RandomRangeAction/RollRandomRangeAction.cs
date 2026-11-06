using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.BattleActions.RandomRangeAction
{
    public class RollRandomRangeAction : SimpleEventBattleAction<RandomRangeEventArgs>
    {
        public RollRandomRangeAction(GameEntity source, int min, int max, bool allowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false, bool isReroll = false)
        {
            base.Args = new RandomRangeEventArgs
            {
                Source = source,
                Min = min,
                Max = max,
                AllowLuck = allowLuck,
                AllowOrderChaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                IsReroll = isReroll
            };
        }

        protected override void PreEventPhase()
        {
            Trigger(RNGCardEventsManager.RangeRolling);

            if (base.Args.Min < 0 && !base.Args.AllowNegativeValues)
                base.Args.Min = 0;

            base.Args.Median = (int)Math.Round((base.Args.Min + base.Args.Max) / 2.0, 0, MidpointRounding.AwayFromZero);

            if (base.Args.Min > base.Args.Median)
                base.Args.Min = base.Args.Median;

            if (base.Args.Max < base.Args.Median)
                base.Args.Max = base.Args.Median;
        }

        protected override void MainPhase()
        {
            base.Args.Result = base.Battle.GameRun.BattleRng.NextInt(base.Args.Min, base.Args.Max);
        }

        protected override void PostEventPhase()
        {
            Trigger(RNGCardEventsManager.RangeRolled);
        }
    }
}
