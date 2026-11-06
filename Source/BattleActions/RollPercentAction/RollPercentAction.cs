using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;

namespace RNGCardPack.Source.BattleActions.RollPercentAction
{
    public class RollPercentAction : SimpleEventBattleAction<PercentEventArgs>
    {
        public RollPercentAction(GameEntity source, int value, bool allowLuck = true, bool isReroll = false)
        {
            base.Args = new PercentEventArgs
            {
                Source = source,
                Value = value,
                AllowLuck = allowLuck,
                IsReroll = isReroll
            };
        }

        protected override void PreEventPhase()
        {
            Trigger(RNGCardEventsManager.PercentRolling);
        }

        protected override void MainPhase()
        {
            int roll = base.Battle.GameRun.BattleRng.NextInt(0, 100);

            if (roll <= base.Args.Value)
                base.Args.Result = true;

            else
                base.Args.Result = false;
        }

        protected override void PostEventPhase()
        {
            Trigger(RNGCardEventsManager.PercentRolled);
        }
    }
}
