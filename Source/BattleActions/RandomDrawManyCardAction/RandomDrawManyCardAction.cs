using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomDrawManyCardAction
{
    public class RandomDrawManyCardAction : SimpleEventBattleAction<RandomCardEventArgs>
    {
        public RandomDrawManyCardAction(Unit source, int minCards, int maxCards, bool allowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false)
        {
            base.Args = new RandomCardEventArgs
            {
                Source = source,
                MinCards = minCards,
                MaxCards = maxCards,
                AllowLuck = allowLuck,
                AllowOrderChaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                RollActions = new Dictionary<string, RollRandomRangeAction>()
            };
        }

        protected override void PreEventPhase()
        {
            Trigger(RNGCardEventsManager.CardRangeDrawing);

            if (base.Args.MinCards < 0 && !base.Args.AllowNegativeValues)
                base.Args.MinCards = 0;

            base.Args.MedianCards = (int)Math.Round((base.Args.MinCards + base.Args.MaxCards) / 2.0, 0, MidpointRounding.AwayFromZero);

            if (base.Args.MinCards > base.Args.MedianCards)
                base.Args.MinCards = base.Args.MedianCards;

            if (base.Args.MaxCards < base.Args.MedianCards)
                base.Args.MaxCards = base.Args.MedianCards;
        }

        protected override void MainPhase()
        {
            base.Args.RollActions.Add(nameof(base.Args.Result), new RollRandomRangeAction(base.Args.Source, base.Args.MinCards, base.Args.MaxCards, base.Args.AllowLuck, false, base.Args.AllowNegativeValues));

            foreach (var element in base.Args.RollActions)
                React(element.Value);
        }

        protected override void PostEventPhase()
        {
            foreach (var element in base.Args.RollActions)
            {
                switch (element.Key)
                {
                    case nameof(base.Args.Result):
                        base.Args.Result = element.Value.Args.Result;
                        break;

                    default:
                        throw new ArgumentException(nameof(element.Key), "Dictionary key did not match any existing result variables");
                }
            }

            React(new DrawManyCardAction(base.Args.Result));

            Trigger(RNGCardEventsManager.CardRangeDrawn);
        }
    }
}
