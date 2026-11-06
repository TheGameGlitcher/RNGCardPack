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
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.G
{
    public sealed class ManaConverterDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Green };

            config.Cost = new ManaGroup() { Green = 1, Any = 1 };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Mana = new ManaGroup() { Philosophy = 1 };

            config.UpgradedRelativeKeyword = Keyword.Philosophy;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(ManaConverterDef))]
    public sealed class ManaConverter : RNGCardPackCard
    {
        public ManaConverter() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 1;
            BaseRandomMaxValue1 = 4;

            BaseUpgradedRandomMinValue1 = 1;
            BaseUpgradedRandomMaxValue1 = 5;

            BasePercentValue1 = 30;
            BaseUpgradedPercentValue1 = 30;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            var action1 = new RollRandomRangeAction(base.Battle.Player, RandomMinValue1, RandomMaxValue1, true, AllowOrderChaos, AllowNegativeValues);

            yield return action1;


            ManaGroup finalMana = ManaGroup.Empty;

            for (int i = 0; i < action1.Args.Result; i++)
            {

                if (IsUpgraded)
                {
                    var action2 = new RollPercentAction(base.Battle.Player, PercentValue1, true);

                    yield return action2;

                    if (action2.Args.Result)
                        finalMana += ManaGroup.Single(ManaColor.Philosophy);
                }

                else
                    finalMana += ManaGroup.Single(ManaColors.Colors.Sample(base.GameRun.BattleRng));
            }

            yield return new GainManaAction(finalMana);

            yield break;
        }
    }
}