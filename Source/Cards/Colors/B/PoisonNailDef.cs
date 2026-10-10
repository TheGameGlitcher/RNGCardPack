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

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class PoisonNailDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Cost = new ManaGroup() { Black = 1, Any = 1 };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.SingleEnemy;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(PoisonNailDef))]
    public sealed class PoisonNail : RNGCardPackCard
    {
        public PoisonNail() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 5;
            BaseRandomMaxValue1 = 10;

            BaseUpgradedRandomMinValue1 = 8;
            BaseUpgradedRandomMaxValue1 = 15;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            yield return DebuffAction<Poison>(selector.SelectedEnemy, result);

            yield break;
        }
    }
}