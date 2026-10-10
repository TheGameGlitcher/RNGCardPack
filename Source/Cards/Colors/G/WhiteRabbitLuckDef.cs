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

namespace RNGCardPack.Source.Cards.Colors.G
{
    public sealed class WhiteRabbitLuckDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Green };

            config.Cost = new ManaGroup() { Green = 1, Any = 3 };
            config.UpgradedCost = new ManaGroup() { Green = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            config.Value2 = 1;

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Self;

            config.RelativeEffects = new List<string>() { "LuckSe", "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "LuckSe", "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(WhiteRabbitLuckDef))]
    public sealed class WhiteRabbitLuck : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return BuffAction<LuckSe>(Value1);

            yield return BuffAction<WhiteRabbitLuckSe>(Value2);

            yield break;
        }
    }
}