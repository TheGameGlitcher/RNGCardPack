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

namespace RNGCardPack.Source.Cards.Colors.WU
{
    public sealed class CirnosPlanDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.White, ManaColor.Blue };

            config.Cost = new ManaGroup() { White = 1, Blue = 1, Any = 1 };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;

            config.TargetType = TargetType.Self;

            config.Shield = 0;

            config.Keywords = Keyword.Shield;
            config.UpgradedKeywords = Keyword.Shield;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(CirnosPlanDef))]
    public sealed class CirnosPlan : RNGCardPackCard
    {
        public CirnosPlan() : base()
        {
            IsRandom = true;

            BaseRandomMinValue1 = 2;
            BaseRandomMaxValue1 = 4;

            BaseUpgradedRandomMinValue1 = 2;
            BaseUpgradedRandomMaxValue1 = 6;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            Card card = base.Battle.DrawZone.Sample(base.BattleRng);

            if (card == null)
                yield break;

            yield return PerformAction.ViewCard(card);

            if (card.Cost.Total <= 0)
                yield break;

            int barrier = card.Cost.Total * RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            yield return new CastBlockShieldAction(base.Battle.Player, 0, barrier);

            yield break;
        }
    }
}