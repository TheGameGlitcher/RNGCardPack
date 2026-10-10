using LBoL.Core.StatusEffects;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoLEntitySideloader.CustomKeywords;
using RNGCardPack.Source.Cards.Tools;
using RNGCardPack.Source.Keywords;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using LBoL.Base;
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

        public virtual bool IsUpgraded { get; protected set; } = false;

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
                if (!((double)(RandomMaxDamage.Amount + (float)Battle.Player.TotalFirepower) - ChaosLevel > (double)BaseRandomMaxDamage * 1.5) && !IsUpgraded)
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

        protected virtual int BaseRandomMinDamage { get; set; } = 0;
        public DamageInfo RandomMinDamage
        {
            get
            {
                DamageInfo finalDamage = DamageInfo.Attack(BaseRandomMinDamage);

                finalDamage.Damage -= ChaosLevel;

                if (finalDamage.Damage >= MedianDamage)
                    finalDamage.Damage = MedianDamage;

                if (finalDamage.Damage < 0)
                    finalDamage.Damage = 0;

                return finalDamage;
            }
        }

        protected virtual int BaseRandomMaxDamage { get; set; } = 0;
        public DamageInfo RandomMaxDamage
        {
            get
            {
                DamageInfo finalDamage = DamageInfo.Attack(BaseRandomMaxDamage);

                finalDamage.Damage += ChaosLevel;

                if (finalDamage.Damage <= MedianDamage)
                    finalDamage.Damage = MedianDamage;

                return finalDamage;
            }
        }

        public int MedianDamage
        {
            get
            {
                return (int)Math.Round(((float)BaseRandomMinDamage + (float)BaseRandomMaxDamage) / 2);
            }
        }

        protected virtual int BaseRandomMinBlock { get; set; } = 0;
        public BlockInfo RandomMinBlock
        {
            get
            {
                BlockInfo finalBlock = new BlockInfo(BaseRandomMinBlock);

                finalBlock.Block -= ChaosLevel;

                if (finalBlock.Block >= MedianBlock)
                    finalBlock.Block = MedianBlock;

                if (finalBlock.Block < 0)
                    finalBlock.Block = 0;

                return finalBlock;
            }
        }

        protected virtual int BaseRandomMaxBlock { get; set; } = 0;
        public BlockInfo RandomMaxBlock
        {
            get
            {
                BlockInfo finalBlock = new BlockInfo(BaseRandomMaxBlock);

                finalBlock.Block += ChaosLevel;

                if (finalBlock.Block <= MedianBlock)
                    finalBlock.Block = MedianBlock;

                return finalBlock;
            }
        }

        public int MedianBlock
        {
            get
            {
                return (int)Math.Round(((float)BaseRandomMinBlock + (float)BaseRandomMaxBlock) / 2);
            }
        }

        protected virtual int BaseRandomMinShield { get; set; } = 0;
        public ShieldInfo RandomMinShield
        {
            get
            {
                ShieldInfo finalShield = new ShieldInfo(BaseRandomMinShield);

                finalShield.Shield -= ChaosLevel;

                if (finalShield.Shield >= MedianShield)
                    finalShield.Shield = MedianShield;

                if (finalShield.Shield < 0)
                    finalShield.Shield = 0;

                return finalShield;
            }
        }

        protected virtual int BaseRandomMaxShield { get; set; } = 0;
        public ShieldInfo RandomMaxShield
        {
            get
            {
                ShieldInfo finalShield = new ShieldInfo(BaseRandomMaxShield);

                finalShield.Shield += ChaosLevel;

                if (finalShield.Shield <= MedianShield)
                    finalShield.Shield = MedianShield;

                return finalShield;
            }
        }

        public int MedianShield
        {
            get
            {
                return (int)Math.Round(((float)BaseRandomMinShield + (float)BaseRandomMaxShield) / 2);
            }
        }

        protected virtual int BaseRandomMinValue1 { get; set; } = 0;
        public int RandomMinValue1
        {
            get
            {
                int finalValue = BaseRandomMinValue1;

                finalValue -= ChaosLevel;

                if (finalValue >= MedianValue1)
                    finalValue = MedianValue1;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        protected virtual int BaseRandomMaxValue1 { get; set; } = 0;
        public int RandomMaxValue1
        {
            get
            {
                int finalValue = BaseRandomMaxValue1;

                finalValue += ChaosLevel;

                if (finalValue <= MedianValue1)
                    finalValue = MedianValue1;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        public int MedianValue1
        {
            get
            {
                return (int)Math.Round(((float)BaseRandomMinValue1 + (float)BaseRandomMaxValue1) / 2);
            }
        }

        protected virtual int BaseRandomMinValue2 { get; set; } = 0;
        public int RandomMinValue2
        {
            get
            {
                int finalValue = BaseRandomMinValue2;

                finalValue -= ChaosLevel;

                if (finalValue >= MedianValue2)
                    finalValue = MedianValue2;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        protected virtual int BaseRandomMaxValue2 { get; set; } = 0;
        public int RandomMaxValue2
        {
            get
            {
                int finalValue = BaseRandomMaxValue2;

                finalValue += ChaosLevel;

                if (finalValue <= MedianValue2)
                    finalValue = MedianValue2;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        public int MedianValue2
        {
            get
            {
                return (int)Math.Round(((float)BaseRandomMinValue2 + (float)BaseRandomMaxValue2) / 2);
            }
        }

        public virtual int PercentValue1 { get; protected set; } = 0;

        public virtual int PercentValue2 { get; protected set; } = 0;

        public void NotifyPercentRollFailed()
        {
            PercentRollFailed?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyPercentRollSucceeded()
        {
            PercentRollSucceeded?.Invoke(this, EventArgs.Empty);
        }

        protected BattleAction RNGAttackActionSe(Unit target, DamageInfo RNGMin, DamageInfo RNGMax, bool isAccurate = false, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);

            if (allowLuck && LuckLevel != 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result >= RNGMax.Amount / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result <= RNGMax.Amount / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);
                    }
                }
            }

            DamageInfo damage = DamageInfo.Attack(result, isAccurate);

            return new DamageAction(base.Owner, target, damage, RNGGunName);
        }

        protected BattleAction RNGAttackActionSe(IEnumerable<Unit> targets, DamageInfo RNGMin, DamageInfo RNGMax, bool isAccurate = false, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);

            if (allowLuck && LuckLevel != 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result >= RNGMax.Amount / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result <= RNGMax.Amount / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Amount, (int)RNGMax.Amount);
                    }
                }
            }

            DamageInfo damage = DamageInfo.Attack(result, isAccurate);

            return new DamageAction(base.Owner, targets, damage, RNGGunName);
        }

        protected BlockInfo RNGBlockRangeSe(BlockInfo RNGMin, BlockInfo RNGMax, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt(RNGMin.Block, RNGMax.Block);

            if (allowLuck && LuckLevel != 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result >= RNGMax.Block / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Block, (int)RNGMax.Block);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result <= RNGMax.Block / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Block, (int)RNGMax.Block);
                    }
                }
            }

            BlockInfo block = new BlockInfo(result);

            return block;
        }

        protected ShieldInfo RNGShieldRangeSe(ShieldInfo RNGMin, ShieldInfo RNGMax, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt(RNGMin.Shield, RNGMax.Shield);

            if (allowLuck && LuckLevel > 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result >= RNGMax.Shield / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Shield, (int)RNGMax.Shield);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result <= RNGMax.Shield / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt((int)RNGMin.Shield, (int)RNGMax.Shield);
                    }
                }
            }

            ShieldInfo shield = new ShieldInfo(result);

            return shield;
        }

        protected int RNGGenericRangeSe(int RNGMin, int RNGMax, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt(RNGMin, RNGMax);

            if (allowLuck && LuckLevel != 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result >= RNGMax / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt(RNGMin, RNGMax);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result <= RNGMax / 2)
                            break;

                        else
                            result = base.GameRun.BattleCardRng.NextInt(RNGMin, RNGMax);
                    }
                }
            }

            return result;
        }

        protected bool RNGGenericPercentSe(int percentValue, bool allowLuck = false, bool allowEvents = true)
        {
            int result = base.GameRun.BattleCardRng.NextInt(1, 100);

            if (allowLuck && LuckLevel != 0)
            {
                if (LuckLevel > 0)
                {
                    for (int rolls = 0; rolls < LuckLevel; rolls++)
                    {
                        if (result <= percentValue)
                        {
                            if (allowEvents)
                                NotifyPercentRollSucceeded();

                            return true;
                        }

                        else
                            result = base.GameRun.BattleCardRng.NextInt(1, 100);
                    }
                }

                else
                {
                    for (int rolls = 0; rolls < -(LuckLevel); rolls++)
                    {
                        if (result > percentValue)
                        {
                            if (allowEvents)
                                NotifyPercentRollFailed();

                            return false;
                        }

                        else
                            result = base.GameRun.BattleCardRng.NextInt(1, 100);
                    }
                }
            }

            if (result <= percentValue)
            {
                if (allowEvents)
                    NotifyPercentRollSucceeded();

                return true;
            }
                

            else
            {
                if (allowEvents)
                    NotifyPercentRollFailed();

                return false;
            }
        }
    }
}
