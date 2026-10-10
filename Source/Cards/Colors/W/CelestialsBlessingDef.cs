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

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class CelestialsBlessingDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { White = 1, Any = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.RelativeKeyword = Keyword.Upgrade;
            config.UpgradedRelativeKeyword = Keyword.Upgrade;

            config.RelativeEffects = new List<string>() { "LuckSe", "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "LuckSe", "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(CelestialsBlessingDef))]
    public sealed class CelestialsBlessing : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return UpgradeAllHandsAction();

            List<Card> rareCards = (from card in base.GameRun.BaseDeckWithoutUnremovable
                                    where card.Config.Rarity == Rarity.Rare
                                    select card).ToList<Card>();

            int finalValue = rareCards.Count * Value1;

            if (finalValue > 0)
                yield return BuffAction<LuckSe>(finalValue);

            yield break;
        }
    }
}