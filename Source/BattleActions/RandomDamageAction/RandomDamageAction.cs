using LBoL.Base;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RNGCardPack.Source.BattleActions.RandomDamageAction
{
    public class RandomDamageAction : SimpleEventBattleAction<RandomDamageEventArgs>
    {
        public RandomDamageAction(Unit source, Unit[] targets, int minDamage, int maxDamage, DamageType damageType, bool isAccuracy = false, string gunName = "Instant", GunType gunType = GunType.Single, bool allowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false)
        {
            base.Args = new RandomDamageEventArgs
            {
                Source = source,
                Targets = targets,
                MinDamage = minDamage,
                MaxDamage = maxDamage,
                DamageType = damageType,
                IsAcurracy = isAccuracy,
                GunName = gunName,
                GunType = gunType,
                AllowLuck = allowLuck,
                AllowOrderchaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                RollActions = new Dictionary<string, RollRandomRangeAction>()
            };
        }

        public RandomDamageAction(Unit source, Unit target, int minDamage, int maxDamage, DamageType damageType, bool isAccuracy = false, string gunName = "Instant", GunType gunType = GunType.Single, bool allowLuck = true, bool allowOrderChaos = true, bool allowNegativeValues = false)
        {
            base.Args = new RandomDamageEventArgs
            {
                Source = source,
                Targets = new Unit[] { target },
                MinDamage = minDamage,
                MaxDamage = maxDamage,
                DamageType = damageType,
                IsAcurracy = isAccuracy,
                GunName = gunName,
                GunType = gunType,
                AllowLuck = allowLuck,
                AllowOrderchaos = allowOrderChaos,
                AllowNegativeValues = allowNegativeValues,
                RollActions = new Dictionary<string, RollRandomRangeAction>()
            };
        }

        protected override void PreEventPhase()
        {
            Trigger(RNGCardEventsManager.DamageRangeDealing);

            if (base.Args.MinDamage < 0 && !base.Args.AllowNegativeValues)
                base.Args.MinDamage = 0;

            base.Args.MedianDamage = (int)Math.Round((base.Args.MinDamage + base.Args.MaxDamage) / 2.0, 0, MidpointRounding.AwayFromZero);

            if (base.Args.MinDamage > base.Args.MedianDamage)
                base.Args.MinDamage = base.Args.MedianDamage;

            if (base.Args.MaxDamage < base.Args.MedianDamage)
                base.Args.MaxDamage = base.Args.MedianDamage;
        }

        protected override void MainPhase()
        {
            base.Args.RollActions.Add(nameof(base.Args.Result), new RollRandomRangeAction(base.Args.Source, base.Args.MinDamage, base.Args.MaxDamage, base.Args.AllowLuck, false, base.Args.AllowNegativeValues));

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

            if (base.Args.Targets.Length > 1)
                React(new DamageAction(base.Args.Source, base.Args.Targets, new DamageInfo(base.Args.Result, base.Args.DamageType, false, base.Args.IsAcurracy), base.Args.GunName, base.Args.GunType));

            else 
                React(new DamageAction(base.Args.Source, base.Args.Targets[0], new DamageInfo(base.Args.Result, base.Args.DamageType, false, base.Args.IsAcurracy), base.Args.GunName, base.Args.GunType));

            Trigger(RNGCardEventsManager.DamageRangeDealt);
        }
    }
}
