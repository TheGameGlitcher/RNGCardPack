using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction
{
    public class RandomCastBlockShieldAction : SimpleEventBattleAction<RandomBlockShieldEventArgs>
    {
        public bool Cast { get; }

        public RandomCastBlockShieldAction(Unit source, Unit target, int minBlock, int maxBlock, int minShield, int maxShield, bool AllowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false, BlockShieldType type = BlockShieldType.Normal, bool cast = true)
        {
            base.Args = new RandomBlockShieldEventArgs
            {
                Source = source,
                Target = target,
                MinBlock = minBlock,
                MaxBlock = maxBlock,
                MinShield = minShield,
                MaxShield = maxShield,
                AllowLuck = AllowLuck,
                AllowOrderChaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                Type = type,
                HasBlock = (maxBlock > 0),
                HasShield = (maxShield > 0),
                RollActions = new Dictionary<string, RollRandomRangeAction>()
            };

            Cast = cast;
        }

        protected override void PreEventPhase()
        {
            Debug.Log("Pre");
            if (Cast)
                Trigger(RNGCardEventsManager.BlockShieldRangeCasting);

            Trigger(RNGCardEventsManager.BlockShieldRangeGaining);

            if (base.Args.HasBlock)
            {
                if (base.Args.MinBlock < 0 && !base.Args.AllowNegativeValues)
                    base.Args.MinBlock = 0;

                base.Args.MedianBlock = (int)Math.Round((base.Args.MinBlock + base.Args.MaxBlock) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinBlock > base.Args.MedianBlock)
                    base.Args.MinBlock = base.Args.MedianBlock;

                if (base.Args.MaxBlock < base.Args.MedianBlock)
                    base.Args.MaxBlock = base.Args.MedianBlock;
            }

            if (base.Args.HasShield)
            {
                if (base.Args.MinShield < 0 && !base.Args.AllowNegativeValues)
                    base.Args.MinShield = 0;

                base.Args.MedianShield = (int)Math.Round((base.Args.MinShield + base.Args.MaxShield) / 2.0, 0, MidpointRounding.AwayFromZero);

                if (base.Args.MinShield > base.Args.MedianShield)
                    base.Args.MinShield = base.Args.MedianShield;

                if (base.Args.MaxShield < base.Args.MedianShield)
                    base.Args.MaxShield = base.Args.MedianShield;
            }
        }

        protected override void MainPhase()
        {
            if (base.Args.HasBlock)
                base.Args.RollActions.Add(nameof(base.Args.BlockResult), new RollRandomRangeAction(base.Args.Source, base.Args.MinBlock, base.Args.MaxBlock, base.Args.AllowLuck, false, base.Args.AllowNegativeValues));
                
            if (base.Args.HasShield)
                base.Args.RollActions.Add(nameof(base.Args.ShieldResult), new RollRandomRangeAction(base.Args.Source, base.Args.MinShield, base.Args.MaxShield, base.Args.AllowLuck, false, base.Args.AllowNegativeValues));

            foreach (var element in base.Args.RollActions)
                React(element.Value);
        }

        protected override void PostEventPhase()
        {
            foreach (var element in base.Args.RollActions)
            {
                switch (element.Key)
                {
                    case nameof(base.Args.BlockResult):
                        base.Args.BlockResult = element.Value.Args.Result;
                        break;

                    case nameof(base.Args.ShieldResult):
                        base.Args.ShieldResult = element.Value.Args.Result;
                        break;

                    default:
                        throw new ArgumentException(nameof(element.Key), "Dictionary key did not match any existing result variables");
                }
            }

            React(new CastBlockShieldAction(base.Args.Source, base.Args.Target, base.Args.BlockResult, base.Args.ShieldResult, base.Args.Type, Cast));

            if (Cast)
                Trigger(RNGCardEventsManager.BlockShieldRangeCasted);

            Trigger(RNGCardEventsManager.BlockShieldRangeGainined);
        }
    }
}
