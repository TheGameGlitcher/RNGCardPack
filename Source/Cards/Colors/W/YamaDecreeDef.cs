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
    public sealed class YamaDecreeDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { White = 1, Any = 2 };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 4;

            config.Value2 = 2;
            config.UpgradedValue2 = 3;

            config.RelativeEffects = new List<string>() { "OrderSe" };
            config.UpgradedRelativeEffects = new List<string>() { "OrderSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(YamaDecreeDef))]
    public sealed class YamaDecree : RNGCardPackCard
    {
        public YamaDecree() : base()
        {
            BaseValue3 = 1;
            BaseUpgradedValue3 = 2;
        }

        public override Interaction Precondition()
        {
            List<YamaDecree> list = Library.CreateCards<YamaDecree>(2, this.IsUpgraded).ToList<YamaDecree>();

            YamaDecree yamaDecree = list[0];
            YamaDecree yamaDecree2 = list[1];

            yamaDecree.ChoiceCardIndicator = 1;
            yamaDecree2.ChoiceCardIndicator = 2;

            yamaDecree.SetBattle(base.Battle);
            yamaDecree2.SetBattle(base.Battle);

            return new MiniSelectCardInteraction(list, false, false, false);
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            MiniSelectCardInteraction miniSelectCardInteraction = (MiniSelectCardInteraction)precondition;
            Card card = miniSelectCardInteraction?.SelectedCard;

            if (card != null)
            {
                if (card.ChoiceCardIndicator == 1)
                {
                    yield return new ApplyStatusEffectAction<OrderSe>(base.Battle.Player, Value1);
                    yield return new ApplyStatusEffectAction<Firepower>(base.Battle.Player, Value2);
                }

                else
                {
                    yield return new ApplyStatusEffectAction<OrderSe>(base.Battle.Player, Value1);
                    yield return new ApplyStatusEffectAction<Spirit>(base.Battle.Player, Value3);
                }
            }

            yield break;
        }
    }
}
