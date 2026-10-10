using LBoL.Base;
using LBoL.Base.Extensions;
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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RNGCardPack.Source.Cards.Template
{
    public class RNGCardPackCard : Card
    {

        public static readonly List<Type> DiceCards = new List<Type>
            {
                typeof(D6),
                typeof(D8),
                typeof(D20),
                typeof(D100)
            };

        public static List<int> GenericCostCards { get; set; } = new List<int>();

        public virtual bool IsRandom { get; protected set; } = false;

        public virtual bool AllowOrderChaos { get; protected set; } = false;

        public virtual bool AllowNegativeValues { get; protected set; } = false;

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
                if (RNGUseBurstGun && !Config.GunNameBurst.IsNullOrEmpty())
                    return Config.GunNameBurst;


                else
                    return Config.GunName;
            }
        }

        protected ManaGroup BaseMana2 { get; set; }
        protected ManaGroup BaseUpgradedMana2 { get; set; }
        public ManaGroup Mana2
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedMana2 : BaseMana2;
            }
        }

        protected virtual int BaseValue3 { get; set; } = 0;
        protected virtual int BaseUpgradedValue3 { get; set; } = 0;
        public int DeltaValue3 { get; protected set; } = 0;
        public int Value3
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue3 : BaseValue3) + DeltaValue3;

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

        protected virtual int BaseValue4 { get; set; } = 0;
        protected virtual int BaseUpgradedValue4 { get; set; } = 0;
        public int DeltaValue4 { get; protected set; } = 0;
        public int Value4
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue4 : BaseValue4) + DeltaValue4;

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

        protected virtual int BaseValue5 { get; set; } = 0;
        protected virtual int BaseUpgradedValue5 { get; set; } = 0;
        public int DeltaValue5 { get; protected set; } = 0;
        public int Value5
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue5 : BaseValue5) + DeltaValue5;

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

        protected virtual int BaseValue6 { get; set; } = 0;
        protected virtual int BaseUpgradedValue6 { get; set; } = 0;
        public int DeltaValue6 { get; protected set; } = 0;
        public int Value6
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue6 : BaseValue6) + DeltaValue6;

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

        protected virtual int BaseValue7 { get; set; } = 0;
        protected virtual int BaseUpgradedValue7 { get; set; } = 0;
        public int DeltaValue7 { get; protected set; } = 0;
        public int Value7
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue7 : BaseValue7) + DeltaValue7;

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

        protected virtual int BaseValue8 { get; set; } = 0;
        protected virtual int BaseUpgradedValue8 { get; set; } = 0;
        public int DeltaValue8 { get; protected set; } = 0;
        public int Value8
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue8 : BaseValue8) + DeltaValue8;

                if (AllowNegativeValues)
                    return ((IsUpgraded) ? BaseUpgradedValue8 : BaseValue8) + DeltaValue8;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        protected virtual int BaseValue9 { get; set; } = 0;
        protected virtual int BaseUpgradedValue9 { get; set; } = 0;
        public int DeltaValue9 { get; protected set; } = 0;
        public int Value9
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue9 : BaseValue9) + DeltaValue9;

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

        protected virtual int BaseValue10 { get; set; } = 0;
        protected virtual int BaseUpgradedValue10 { get; set; } = 0;
        public int DeltaValue10 { get; protected set; } = 0;
        public int Value10
        {
            get
            {
                int finalValue = ((IsUpgraded) ? BaseUpgradedValue10 : BaseValue10) + DeltaValue10;

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

        protected virtual int BaseRandomMinDamage { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinDamage { get; set; } = 0;
        public DamageInfo RandomMinDamage
        {
            get
            {
                DamageInfo finalDamage = DamageInfo.Attack((IsUpgraded) ? BaseUpgradedRandomMinDamage : BaseRandomMinDamage);

                finalDamage.Damage -= ChaosLevel;

                if (finalDamage.Damage >= MedianDamage)
                    finalDamage.Damage = MedianDamage;

                if (finalDamage.Damage < 0)
                    finalDamage.Damage = 0;

                return finalDamage;
            }
        }

        protected virtual int BaseRandomMaxDamage { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxDamage { get; set; } = 0;
        public DamageInfo RandomMaxDamage
        {
            get
            {
                DamageInfo finalDamage = DamageInfo.Attack((IsUpgraded) ? BaseUpgradedRandomMaxDamage : BaseRandomMaxDamage);

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
                 return (int)Math.Round(
                     (((IsUpgraded) ? (float)BaseUpgradedRandomMinDamage : (float)BaseRandomMinDamage) +
                     ((IsUpgraded) ? (float)BaseUpgradedRandomMaxDamage : (float)BaseRandomMaxDamage)) /
                     2);
            }
        }

        protected virtual int BaseRandomMinBlock { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinBlock { get; set; } = 0;
        public BlockInfo RandomMinBlock
        {
            get
            {
                BlockInfo finalBlock = new BlockInfo((IsUpgraded) ? BaseUpgradedRandomMinBlock : BaseRandomMinBlock);

                finalBlock.Block -= ChaosLevel;

                if (finalBlock.Block >= MedianBlock)
                    finalBlock.Block = MedianBlock;

                if (finalBlock.Block < 0)
                    finalBlock.Block = 0;

                return finalBlock;
            }
        }

        protected virtual int BaseRandomMaxBlock { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxBlock { get; set; } = 0;
        public BlockInfo RandomMaxBlock
        {
            get
            {
                BlockInfo finalBlock = new BlockInfo((IsUpgraded) ? BaseUpgradedRandomMaxBlock : BaseRandomMaxBlock);

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
                return (int)Math.Round(
                    (((IsUpgraded) ? (float)BaseUpgradedRandomMinBlock : (float)BaseRandomMinBlock) +
                    ((IsUpgraded) ? (float)BaseUpgradedRandomMaxBlock : (float)BaseRandomMaxBlock)) /
                    2);
            }
        }

        protected virtual int BaseRandomMinShield { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinShield { get; set; } = 0;
        public ShieldInfo RandomMinShield
        {
            get
            {
                ShieldInfo finalShield = new ShieldInfo((IsUpgraded) ? BaseUpgradedRandomMinShield : BaseRandomMinShield);

                finalShield.Shield -= ChaosLevel;

                if (finalShield.Shield >= MedianShield)
                    finalShield.Shield = MedianShield;

                if (finalShield.Shield < 0)
                    finalShield.Shield = 0;

                return finalShield;
            }
        }

        protected virtual int BaseRandomMaxShield { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxShield { get; set; } = 0;
        public ShieldInfo RandomMaxShield
        {
            get
            {
                ShieldInfo finalShield = new ShieldInfo((IsUpgraded) ? BaseUpgradedRandomMaxShield : BaseRandomMaxShield);

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
                return (int)Math.Round(
                    (((IsUpgraded) ? (float)BaseUpgradedRandomMinShield : (float)BaseRandomMinShield) +
                    ((IsUpgraded) ? (float)BaseUpgradedRandomMaxShield : (float)BaseRandomMaxShield)) /
                    2);
            }
        }

        protected virtual int BaseRandomMinValue1 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinValue1 { get; set; } = 0;
        public int RandomMinValue1
        {
            get
            {
                int finalValue =  (IsUpgraded) ? BaseUpgradedRandomMinValue1 : BaseRandomMinValue1;

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
        protected virtual int BaseUpgradedRandomMaxValue1 { get; set; } = 0;
        public int RandomMaxValue1
        {
            get
            {
                int finalValue = (IsUpgraded) ? BaseUpgradedRandomMaxValue1 : BaseRandomMaxValue1;

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
                return (int)Math.Round(
                    (((IsUpgraded) ? (float)BaseUpgradedRandomMinValue1 : (float)BaseRandomMinValue1) +
                    ((IsUpgraded) ? (float)BaseUpgradedRandomMaxValue1 : (float)BaseRandomMaxValue1)) /
                    2);
            }
        }

        protected virtual int BaseRandomMinValue2 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinValue2 { get; set; } = 0;
        public int RandomMinValue2
        {
            get
            {
                int finalValue = (IsUpgraded) ? BaseUpgradedRandomMinValue2 : BaseRandomMinValue2;

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
        protected virtual int BaseUpgradedRandomMaxValue2 { get; set; } = 0;
        public int RandomMaxValue2
        {
            get
            {
                int finalValue = (IsUpgraded) ? BaseUpgradedRandomMaxValue2 : BaseRandomMaxValue2;

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
                return (int)Math.Round(
                    (((IsUpgraded) ? (float)BaseUpgradedRandomMinValue2 : (float)BaseRandomMinValue2) +
                    ((IsUpgraded) ? (float)BaseUpgradedRandomMaxValue2 : (float)BaseRandomMaxValue2)) /
                    2);
            }
        }

        protected virtual int BasePercentValue1 { get; set; } = 0;
        protected virtual int BaseUpgradedPercentValue1 { get; set; } = 0;
        public int PercentValue1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedPercentValue1;

                else
                    return BasePercentValue1;
            }
        }

        protected virtual int BasePercentValue2 { get; set; } = 0;
        protected virtual int BaseUpgradedPercentValue2 { get; set; } = 0;
        public int PercentValue2
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedPercentValue2;

                else
                    return BasePercentValue2;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            if (IsRandom)
                this.AddCustomKeyword(UnpredictableKeyword.Unpredictable);

            else
                IsRandom = false;
        }

        public void NotifyPercentRollFailed()
        {
            PercentRollFailed?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyPercentRollSucceeded()
        {
            PercentRollSucceeded?.Invoke(this, EventArgs.Empty);
        }

        protected BattleAction RNGAttackAction(Unit target, DamageInfo RNGMin, DamageInfo RNGMax, bool isAccurate = false, bool allowLuck = false)
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

            return new DamageAction(base.Battle.Player, target, damage, RNGGunName);
        }

        protected BattleAction RNGAttackAction(IEnumerable<Unit> targets, DamageInfo RNGMin, DamageInfo RNGMax, bool isAccurate = false, bool allowLuck = false)
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

            return new DamageAction(base.Battle.Player, targets, damage, RNGGunName);
        }

        protected BattleAction RNGBlockShieldAction( BlockInfo RNGMin, BlockInfo RNGMax, bool allowLuck = false)
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

            return DefenseAction(result, 0);
        }

        protected BattleAction RNGBlockShieldAction(ShieldInfo RNGMin, ShieldInfo RNGMax, bool allowLuck = false)
        {
            int result = base.GameRun.BattleCardRng.NextInt(RNGMin.Shield, RNGMax.Shield);

            if (allowLuck && LuckLevel != 0)
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

            return DefenseAction(0, result);
        }

        protected int RNGGenericRange(int RNGMin, int RNGMax, bool allowLuck = false)
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

        protected bool RNGGenericPercent(int percentValue, bool allowLuck = false, bool allowEvents = true)
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