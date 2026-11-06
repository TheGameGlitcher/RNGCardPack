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
using RNGCardPack.Source.Structs;
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
                if (!((double)(RandomMaxDamage + (float)Battle.Player.TotalFirepower) - ChaosLevel > (double)BaseRandomMaxDamage * 1.5) && !IsUpgraded)
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
        public int RandomMinDamage
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMinDamage : BaseRandomMinDamage;
            }

        }

        protected virtual int BaseRandomMaxDamage { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxDamage { get; set; } = 0;
        public int RandomMaxDamage
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMaxDamage : BaseRandomMaxDamage;
            }
        }

        public DamageRangeInfo DamageRange
        {
            get
            {
                return new DamageRangeInfo(RandomMinDamage, RandomMaxDamage, DamageType.Attack, false).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int BaseRandomMinBlock { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinBlock { get; set; } = 0;
        public int RandomMinBlock
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMinBlock : BaseRandomMinBlock;
            }
        }

        protected virtual int BaseRandomMaxBlock { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxBlock { get; set; } = 0;
        public int RandomMaxBlock
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMaxBlock : BaseRandomMaxBlock;
            }
        }

        public BlockRangeInfo BlockRange
        {
            get
            {
                return new BlockRangeInfo(RandomMinBlock, RandomMaxBlock, BlockShieldType.Normal).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int BaseRandomMinShield { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinShield { get; set; } = 0;
        public int RandomMinShield
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMinShield : BaseRandomMinShield;
            }
        }

        protected virtual int BaseRandomMaxShield { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxShield { get; set; } = 0;
        public int RandomMaxShield
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMaxShield : BaseRandomMaxShield;
            }
        }

        public ShieldRangeInfo ShieldRange
        {
            get
            {
                return new ShieldRangeInfo(RandomMinShield, RandomMaxShield, BlockShieldType.Normal).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int BaseRandomMinValue1 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinValue1 { get; set; } = 0;
        public int RandomMinValue1
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMinValue1 : BaseRandomMinValue1;
            }
        }

        protected virtual int BaseRandomMaxValue1 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxValue1 { get; set; } = 0;
        public int RandomMaxValue1
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMaxValue1 : BaseRandomMaxValue1;
            }
        }

        public GenericRangeInfo ValueRange1
        {
            get
            {
                return new GenericRangeInfo(RandomMinValue1, RandomMaxValue1).Shift(ChaosLevel, AllowNegativeValues);
            }
        }

        protected virtual int BaseRandomMinValue2 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMinValue2 { get; set; } = 0;
        public int RandomMinValue2
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMinValue2 : BaseRandomMinValue2;
            }
        }

        protected virtual int BaseRandomMaxValue2 { get; set; } = 0;
        protected virtual int BaseUpgradedRandomMaxValue2 { get; set; } = 0;
        public int RandomMaxValue2
        {
            get
            {
                return (IsUpgraded) ? BaseUpgradedRandomMaxValue2 : BaseRandomMaxValue2;
            }
        }

        public GenericRangeInfo ValueRange2
        {
            get
            {
                return new GenericRangeInfo(RandomMinValue2, RandomMaxValue2).Shift(ChaosLevel, AllowNegativeValues);
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
    }
}