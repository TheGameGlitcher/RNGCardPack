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
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class FragmentedGrimoireDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Cost = new ManaGroup() { Black = 1, Any = 2 };

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;

            config.Value2 = 2;
            config.UpgradedValue2 = 3;

            config.Mana = new ManaGroup() { };

            config.Type = CardType.Ability;

            config.TargetType = TargetType.Nobody;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(FragmentedGrimoireDef))]
    public sealed class FragmentedGrimoire : RNGCardPackCard
    {
        public FragmentedGrimoire() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 1;
            BaseRandomMaxValue1 = 3;

            BaseUpgradedRandomMinValue1 = 0;
            BaseUpgradedRandomMaxValue1 = 2;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return new RandomDamageAction(base.Battle.Player, base.Battle.Player, RandomMinValue1, RandomMaxValue1, DamageType.HpLose, false, GunNameID.GetGunFromId(862), GunType.Single, true, AllowOrderChaos, AllowNegativeValues); // Sacrifice

            if (IsUpgraded)
                yield return BuffAction<FragmentedGrimoireSe>(Value2, 0, 1, Value1);

            else
                yield return BuffAction<FragmentedGrimoireSe>(Value2, 0, 0, Value1);

            yield break;
        }
    }
}