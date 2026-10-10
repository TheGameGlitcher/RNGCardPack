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
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.R
{
    public sealed class PhantomicKillerDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Red };

            config.Cost = new ManaGroup() { Red = 1, Any = 3 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 3;
            config.UpgradedValue1 = 4;

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Self;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(PhantomicKillerDef))]
    public sealed class PhantomicKiller : RNGCardPackCard
    {
        public PhantomicKiller() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 5;
            BaseRandomMaxValue1 = 10;

            BaseUpgradedRandomMinValue1 = 8;
            BaseUpgradedRandomMaxValue1 = 10;

            BasePercentValue1 = 50;
            BaseUpgradedPercentValue1 = 75;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return BuffAction<ChaosSe>(Value1);

            yield return BuffAction<PhantomicKillerSe>(RandomMaxValue1 - ChaosLevel, 0, 0, RandomMinValue1 + ChaosLevel);

            yield break;
        }
    }
}