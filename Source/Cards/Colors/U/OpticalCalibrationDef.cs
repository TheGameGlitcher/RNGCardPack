using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;

namespace RNGCardPack.Source.Cards.Colors.U
{
    public sealed class OpticalCalibrationDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = GunNameID.GetGunFromId(6046);        // 扳手
            config.GunNameBurst = GunNameID.GetGunFromId(777003); // HammerPlayer

            config.Colors = new ManaColor[] { ManaColor.Blue };

            config.Cost = new ManaGroup() { Blue = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Damage = 15;
            config.UpgradedDamage = 20;

            config.Value1 = 2;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.SingleEnemy;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe", "GuangxueMicai" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe", "GuangxueMicai" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(OpticalCalibrationDef))]
    public sealed class OpticalCalibration : RNGCardPackCard
    {
        public OpticalCalibration() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BasePercentValue1 = 10;
            BaseUpgradedPercentValue1 = 15;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return AttackAction(selector.SelectedEnemy);

            if (RNGGenericPercent(PercentValue1, true))
                yield return BuffAction<GuangxueMicai>(0, Value1);

            yield break;
        }
    }
}