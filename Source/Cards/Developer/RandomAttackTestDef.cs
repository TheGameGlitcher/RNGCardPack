using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.Cards.Template;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Developer
{
    public sealed class RandomAttackTestDef : RNGCardPackCardTemplate
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

            config.Damage = 0;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.SingleEnemy;

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(RandomAttackTestDef))]
    public sealed class RandomAttackTest : RNGCardPackCard
    {

        public RandomAttackTest() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinDamage = 5;
            BaseUpgradedRandomMinDamage = 8;

            BaseRandomMaxDamage = 10;
            BaseUpgradedRandomMaxDamage = 13;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new RandomDamageAction(base.Battle.Player, selector.SelectedEnemy, RandomMinDamage, RandomMaxDamage, DamageType.Attack, false, RNGGunName, GunType.Single, true, AllowOrderChaos, AllowNegativeValues);
        }
    }
}