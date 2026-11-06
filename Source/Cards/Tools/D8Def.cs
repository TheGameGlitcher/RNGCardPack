using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.ApplyStatusEffectRangeAction;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Tools
{
    public sealed class D8Def : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.Cost = new ManaGroup() { };

            config.IsUpgradable = false;

            config.Rarity = Rarity.Rare;

            config.Value1 = 0;

            config.ToolPlayableTimes = 1;

            config.Type = CardType.Tool;

            config.TargetType = TargetType.Self;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "TheUltimateTauboy";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(D8Def))]
    public sealed class D8 : RNGCardPackCard
    {

        public D8() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;
            AllowNegativeValues = true;

            BaseRandomMinValue1 = -2;
            BaseRandomMaxValue1 = 4;

            BaseRandomMinValue2 = -2;
            BaseRandomMaxValue2 = 4;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return new RandomApplyStatusEffectAction(typeof(Firepower), base.Battle.Player, RandomMinValue1, RandomMaxValue1, null, null, null, null, null, null, true, 0.2f, true, AllowOrderChaos, AllowNegativeValues);

            yield return new RandomApplyStatusEffectAction(typeof(Spirit), base.Battle.Player, RandomMinValue2, RandomMaxValue2, null, null, null, null, null, null, true, 0.2f, true, AllowOrderChaos, AllowNegativeValues);

            yield break;
        }
    }
}
