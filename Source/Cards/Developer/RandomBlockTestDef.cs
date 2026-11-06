using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.Structs;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Developer
{
    public sealed class RandomBlockTestDef : RNGCardPackCardTemplate
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

            config.Block = 0;

            config.Value1 = 0;

            config.Type = CardType.Defense;

            config.TargetType = TargetType.Self;

            config.Keywords = Keyword.Block;
            config.UpgradedKeywords = Keyword.Block;

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(RandomBlockTestDef))]
    public sealed class RandomBlockTest : RNGCardPackCard
    {

        public RandomBlockTest() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinBlock = 5;
            BaseUpgradedRandomMinBlock = 7;

            BaseRandomMaxBlock = 10;
            BaseUpgradedRandomMaxBlock = 12;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new RandomCastBlockShieldAction(base.Battle.Player, base.Battle.Player, RandomMinBlock, RandomMaxBlock, 0, 0);
        }
    }
}