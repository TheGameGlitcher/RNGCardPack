using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class HonorDefianceAvoidanceDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { White = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            config.Value2 = 4;
            config.UpgradedValue2 = 8;

            config.RelativeEffects = new List<string>() { "OrderSe", "ChaosSe" };
            config.UpgradedRelativeEffects = new List<string>() { "OrderSe", "ChaosSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(HonorDefianceAvoidanceDef))]
    public sealed class HonorDefianceAvoidance : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return new ApplyStatusEffectAction<HonorDefianceAvoidanceSe>(base.Battle.Player, Value1, null, Value2);

            yield break;
        }
    }
}
