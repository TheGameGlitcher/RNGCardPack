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
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.G
{
    public sealed class UncertainFutureDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Blue };

            config.Cost = new ManaGroup() { Blue = 1, Any = 1 };

            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(UncertainFutureDef))]
    public sealed class UncertainFuture : RNGCardPackCard
    {
        public UncertainFuture() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 2;
            BaseRandomMaxValue1 = 5;

            BaseUpgradedRandomMinValue1 = 3;
            BaseUpgradedRandomMaxValue1 = 5;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            yield return new DrawManyCardAction(result);

            yield break;
        }
    }
}