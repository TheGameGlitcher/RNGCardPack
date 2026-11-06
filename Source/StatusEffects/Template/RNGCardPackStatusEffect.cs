using LBoL.Base;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.CustomKeywords;
using RNGCardPack.Source.Cards.Tools;
using RNGCardPack.Source.Keywords;
using RNGCardPack.Source.StatusEffects.Effects;
using RNGCardPack.Source.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace RNGCardPack.Source.StatusEffects.Template
{
    public class RNGCardPackStatusEffect : StatusEffect
    {
        public virtual bool AllowOrderChaos { get; protected set; } = false;

        public virtual bool AllowNegativeValues { get; protected set; } = false;

        public virtual ManaGroup Mana { get; protected set; } = new ManaGroup() { };

        public virtual string GunName { get; protected set; } = null;

        public virtual string GunNameBurst { get; protected set; } = null;

        public event EventHandler PercentRollFailed;
        public event EventHandler PercentRollSucceeded;

        protected int ChaosLevel
        {
            get
            {
                if (Battle == null || !AllowOrderChaos)
                    return 0;

                else
                {
                    ChaosSe chaos = base.Battle.Player.GetStatusEffect<ChaosSe>();
                    OrderSe order = base.Battle.Player.GetStatusEffect<OrderSe>();
                    TempChaosSe tempChaos = base.Battle.Player.GetStatusEffect<TempChaosSe>();
                    TempOrderSe tempOrder = base.Battle.Player.GetStatusEffect<TempOrderSe>();

                    return ((chaos == null) ? 0 : chaos.Level) + ((tempChaos == null) ? 0 : tempChaos.Level) - ((order == null) ? 0 : order.Level) - ((tempOrder == null) ? 0 : tempOrder.Level);
                }
            }
        }

        protected int LuckLevel
        {
            get
            {
                if (Battle == null)
                    return 0;

                else
                {
                    LuckSe luck = base.Battle.Player.GetStatusEffect<LuckSe>();
                    TempLuckSe tempLuck = base.Battle.Player.GetStatusEffect<TempLuckSe>();
                    LuckNegativeSe badLuck = base.Battle.Player.GetStatusEffect<LuckNegativeSe>();
                    TempLuckNegativeSe tempBadLuck = base.Battle.Player.GetStatusEffect<TempLuckNegativeSe>();


                    return (((luck == null) ? 0 : luck.Level) + ((tempLuck == null) ? 0 : tempLuck.Level) - ((badLuck == null) ? 0 : badLuck.Level) + ((tempBadLuck == null) ? 0 : tempBadLuck.Level));
                }
            }
        }

        public bool RNGUseBurstGun
        {
            get
            {
                if (!((double)(RandomMaxDamage + (float)Battle.Player.TotalFirepower) - ChaosLevel > (double)RandomMaxDamage * 1.5) && Limit > 0)
                    return Battle.Player.HasStatusEffect<Burst>();

                return true;
            }
        }

        public string RNGGunName
        {
            get
            {
                if (RNGUseBurstGun && GunNameBurst != "")
                    return GunNameBurst;


                else
                    return GunName;
            }
        }

        public virtual int BaseValue1 { get; protected set; } = 0;
        public int DeltaValue1 { get; protected set; } = 0;
        public int Value1
        {
            get
            {
                return BaseValue1 + DeltaValue1;
            }
        }

        public virtual int BaseValue2 { get; protected set; } = 0;
        public int DeltaValue2 { get; protected set; } = 0;
        public int Value2
        {
            get
            {
                return BaseValue2 + DeltaValue2;
            }
        }

        public virtual int BaseValue3 { get; protected set; } = 0;
        public int DeltaValue3 { get; protected set; } = 0;
        public int Value3
        {
            get
            {
                return BaseValue3 + DeltaValue3;
            }
        }

        public virtual int BaseValue4 { get; protected set; } = 0;
        public int DeltaValue4 { get; protected set; } = 0;
        public int Value4
        {
            get
            {
                return BaseValue4 + DeltaValue4;
            }
        }

        public virtual int BaseValue5 { get; protected set; } = 0;
        public int DeltaValue5 { get; protected set; } = 0;
        public int Value5
        {
            get
            {
                return BaseValue5 + DeltaValue5;
            }
        }

        protected virtual int RandomMinDamage { get; set; } = 0;

        protected virtual int RandomMaxDamage { get; set; } = 0;

        public DamageRangeInfo DamageRange
        {
            get
            {
                return new DamageRangeInfo(RandomMinDamage, RandomMaxDamage, DamageType.Attack, false).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int RandomMinBlock { get; set; } = 0;

        protected virtual int RandomMaxBlock { get; set; } = 0;

        public BlockRangeInfo BlockRange
        {
            get
            {
                return new BlockRangeInfo(RandomMinBlock, RandomMaxBlock, BlockShieldType.Normal).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int RandomMinShield { get; set; } = 0;

        protected virtual int RandomMaxShield { get; set; } = 0;

        public ShieldRangeInfo ShieldRange
        {
            get
            {
                return new ShieldRangeInfo(RandomMinShield, RandomMaxShield, BlockShieldType.Normal).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int RandomMinValue1 { get; set; } = 0;

        protected virtual int RandomMaxValue1 { get; set; } = 0;

        public GenericRangeInfo ValueRange1
        {
            get
            {
                return new GenericRangeInfo(RandomMinValue1, RandomMaxValue1).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int RandomMinValue2 { get; set; } = 0;

        protected virtual int RandomMaxValue2 { get; set; } = 0;

        public GenericRangeInfo ValueRange2
        {
            get
            {
                return new GenericRangeInfo(RandomMinValue2, RandomMaxValue2).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        public virtual int PercentValue1 { get; protected set; } = 0;

        public virtual int PercentValue2 { get; protected set; } = 0;
    }
}
