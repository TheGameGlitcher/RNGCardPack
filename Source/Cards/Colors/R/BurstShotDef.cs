using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.R
{
    public sealed class BurstShotDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = GunNameID.GetGunFromId(30010);
            config.GunNameBurst = GunNameID.GetGunFromId(30011);

            config.Colors = new ManaColor[] { ManaColor.Red };

            config.Cost = new ManaGroup() { Red = 1, Any = 1 };

            config.Rarity = Rarity.Common;

            config.Damage = 10;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.SingleEnemy;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(BurstShotDef))]
    public sealed class BurstShot : RNGCardPackCard
    {
        public BurstShot() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 1;
            BaseRandomMaxValue1 = 2;

            BaseUpgradedRandomMinValue1 = 2;
            BaseUpgradedRandomMaxValue1 = 4;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            for (int i = 0; i < result; i++)
                yield return AttackAction(selector.SelectedEnemy);

            yield break;
        }
    }
}