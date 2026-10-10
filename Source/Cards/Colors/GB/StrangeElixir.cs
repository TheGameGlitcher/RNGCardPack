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

namespace RNGCardPack.Source.Cards.Colors.GB
{
    public sealed class StrangeElixirDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Green, ManaColor.Black };

            config.Cost = new ManaGroup() { Green = 1, Black = 1, Any = 1 };
            config.UpgradedCost = new ManaGroup() { Hybrid = 1, HybridColor = 8, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.Value2 = 2;
            config.UpgradedValue2 = 5;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Self;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.RelativeEffects = new List<string>() { "LuckSe", "ChaosSe", "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "LuckSe", "ChaosSe", "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(StrangeElixirDef))]
    public sealed class StrangeElixir : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return BuffAction<LuckSe>(Value1);

            yield return BuffAction<ChaosSe>(Value2);

            yield break;
        }
    }
}