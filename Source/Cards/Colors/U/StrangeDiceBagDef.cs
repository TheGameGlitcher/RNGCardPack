using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.Patches;
using System.Collections.Generic;
using System.Linq;

namespace RNGCardPack.Source.Cards.Colors.U
{
    public sealed class StrangeDiceBagDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Blue };

            config.Cost = new ManaGroup() { Blue = 1, Any = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 3 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;

            config.Value2 = 4;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.RelativeCards = new List<string>() { "D8", "D6", "D100", "D20" };
            config.UpgradedRelativeCards = new List<string>() { "D8", "D6", "D100", "D20" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(StrangeDiceBagDef))]
    public sealed class StrangeDiceBag : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            List<Card> list = new List<Card>();

            for (int i = 0; i < Value2; i++)
                list.Add(Library.CreateCard(DiceCards[i]));

            list.Shuffle(base.GameRun.BattleCardRng);

            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false) { Source = this };

            yield return new InteractionAction(interaction, false);

            Card selectedCard = interaction.SelectedCard;

            yield return new AddCardsToHandAction(new Card[] { selectedCard });

            yield break;
        }
    }
}