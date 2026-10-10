using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Colors.W;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class MeteonicShowerDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = GunNameID.GetGunFromId(777004);      // MeteonicShower
            config.GunNameBurst = GunNameID.GetGunFromId(777005); // MeteonicShowerB

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { White = 1, Any = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2 };

            config.Rarity = Rarity.Uncommon;

            config.Damage = 12;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.SingleEnemy;

            config.RelativeCards = new List<string>() { "MeteonicDebris" };
            config.UpgradedRelativeCards = new List<string>() { "MeteonicDebris" };

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(MeteonicShowerDef))]
    public sealed class MeteonicShower : RNGCardPackCard
    {
        public MeteonicShower() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinValue1 = 2;
            BaseRandomMaxValue1 = 3;

            BaseUpgradedRandomMinValue1 = 2;
            BaseUpgradedRandomMaxValue1 = 3;

            BasePercentValue1 = 50;
            BaseUpgradedPercentValue1 = 50;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            yield return AttackAction(selector.SelectedEnemy);

            int cardAmount = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

            List<Card> cards = Library.CreateCards<MeteonicDebris>(cardAmount).ToList<Card>();

            if (RNGGenericPercent(PercentValue1, false, false))
                yield return new AddCardsToDiscardAction(cards);

            else
                yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);

            yield break;
        }
    }
}
