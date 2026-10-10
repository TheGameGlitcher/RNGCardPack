using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class PovertyGodsWrathDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Cost = new ManaGroup() { Black = 2, Any = 1 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 2;

            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Self;

            config.RelativeEffects = new List<string> { "LuckNegativeSe" };
            config.UpgradedRelativeEffects = new List<string> { "LuckNegativeSe" };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(PovertyGodsWrathDef))]
    public sealed class PovertyGodsWrath : RNGCardPackCard
    {
        public PovertyGodsWrath() : base()
        {
            IsRandom = true;

            BasePercentValue1 = 50;
            BaseUpgradedPercentValue1 = 50;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return new ApplyStatusEffectAction<LuckNegativeSe>(base.Battle.Player, Value1);

            yield return new ApplyStatusEffectAction<PovertyGodsWrathSe>(base.Battle.Player, Value2, null, 0, null);

            yield break;
        }
    }
}