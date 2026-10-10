using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.Cards.Colors.R
{
    public sealed class RecursiveShotDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = "时间悖论";
            config.GunNameBurst = "时间悖论B";

            config.Colors = new ManaColor[] { ManaColor.Red };

            config.Rarity = Rarity.Common;

            config.Damage = 0;

            config.Value1 = 1;

            config.Value2 = 10;
            config.UpgradedValue2 = 15;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.RandomEnemy;

            config.Keywords = Keyword.Forbidden | Keyword.Replenish;
            config.UpgradedKeywords = Keyword.Forbidden | Keyword.Replenish;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe", "TempFirepower" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe", "TempFirepower" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(RecursiveShotDef))]
    public sealed class RecursiveShot : RNGCardPackCard
    {
        public RecursiveShot() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinDamage = 2;
            BaseRandomMaxDamage = 5;

            BaseUpgradedRandomMinDamage = 2;
            BaseUpgradedRandomMaxDamage = 5;

            BasePercentValue1 = 15;
            BaseUpgradedPercentValue1 = 15;

            BasePercentValue2 = 40;
            BaseUpgradedPercentValue2 = 40;
        }

        public int Counter { get; set; } = 0;

        public override IEnumerable<BattleAction> OnDraw()
        {
            return this.EnterHandReactor(true);
        }

        public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
        {
            if (dstZone != CardZone.Hand)
                return null;

            return this.EnterHandReactor(true);
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            if (base.Zone == CardZone.Hand)
                base.React(this.EnterHandReactor(true));

            Counter = Value2;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            return this.EnterHandReactor(false);
        }

        private IEnumerable<BattleAction> EnterHandReactor(bool ensureInHand = false)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (ensureInHand && base.Zone != CardZone.Hand)
            {
                Debug.LogWarning(this.Name + " is not in hand.");
                yield break;
            }

            yield return RNGAttackAction(base.Battle.RandomAliveEnemy, RandomMinDamage, RandomMaxDamage, false, true);

            if (RNGGenericPercent(PercentValue1, true))
                yield return BuffAction<TempFirepower>(Value1);

            Counter--;

            if (Counter <= 0)
                yield return new RemoveCardAction(this);

            else
            {
                if (RNGGenericPercent(BasePercentValue2, true))
                    yield return new MoveCardToDrawZoneAction(this, DrawZoneTarget.Random);

                else
                    yield return new DiscardAction(this);
            }

            yield break;
        }
    }
}
