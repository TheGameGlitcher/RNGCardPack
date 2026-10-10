using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Developer
{
    public sealed class RandomDrawTestDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.DebugLevel = 3;

            config.HideMesuem = true;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.Cost = new ManaGroup() { Any = 1 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(RandomDrawTestDef))]
    public sealed class RandomDrawTest : RNGCardPackCard
    {

        public RandomDrawTest() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 1;
            BaseUpgradedRandomMinValue1 = 1;

            BaseRandomMaxValue1 = 2;
            BaseUpgradedRandomMaxValue1 = 3;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            int result = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            if (result < 0)
            {
                Card[] discard = base.Battle.HandZone.SampleMany(-result, base.BattleRng);

                yield return new DiscardManyAction(discard);
                yield break;
            }

            else if (result > 0)
            {
                yield return new DrawManyCardAction(result);
                yield break;
            }

            else
               yield break;
        }
    }
}
