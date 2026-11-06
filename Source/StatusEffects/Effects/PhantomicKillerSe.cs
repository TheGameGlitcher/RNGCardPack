using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class PhantomicKillerSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.LevelStackType = StackType.Keep;

            config.ShowPlusByLimit = true;

            config.LimitStackType = StackType.Max;

            config.HasCount = true;

            config.CountStackType = StackType.Keep;

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(PhantomicKillerSeDef))]
    public sealed class PhantomicKillerSe : RNGCardPackStatusEffect
    {
        public PhantomicKillerSe() : base()
        {
            AllowOrderChaos = true;

            GunName = GunNameID.GetGunFromId(4034);
            GunNameBurst = GunNameID.GetGunFromId(4035);

            PercentValue1 = 50;
            PercentValue2 = 75;
        }

        public int PercentValue
        {
            get
            {
                return (Limit > 0) ? PercentValue2 : PercentValue1;
            }
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.ReactOwnerEvent<StatusEffectApplyEventArgs>(base.Owner.StatusEffectAdded, new EventSequencedReactor<StatusEffectApplyEventArgs>(this.OnStatusEffectAdded));

            RandomMinDamage = Count;
            RandomMaxDamage = Level;

            Count = Math.Max(DamageRange.MinDamage + base.Battle.Player.TotalFirepower, 0);
            Level = Math.Max(DamageRange.MaxDamage + base.Battle.Player.TotalFirepower, 0);
        }

        private IEnumerable<BattleAction> OnStatusEffectAdded(StatusEffectApplyEventArgs args)
        {
            Count = Math.Max(DamageRange.MinDamage + base.Battle.Player.TotalFirepower, 0);
            Level = Math.Max(DamageRange.MaxDamage + base.Battle.Player.TotalFirepower, 0);

            yield break;
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            var action = new RollPercentAction(this.Owner, PercentValue, true);

            yield return action;

            if (action.Args.Result)
            {
                this.NotifyActivating();

                yield return new RandomDamageAction(this.Owner, base.Battle.RandomAliveEnemy, RandomMinDamage, RandomMaxDamage, DamageType.Attack, false, GunName, GunType.Single, true, AllowOrderChaos, AllowNegativeValues);
            }

            yield break;
        }
    }

}