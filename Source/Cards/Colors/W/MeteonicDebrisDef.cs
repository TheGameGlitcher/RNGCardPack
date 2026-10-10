using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class MeteonicDebrisDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = GunNameID.GetGunFromId(777006);      // MeteonicDebris
            config.GunNameBurst = GunNameID.GetGunFromId(777007); // MeteonicDebrisB

            config.IsPooled = false;

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Rarity = Rarity.Common;

            config.Damage = 0;

            config.Type = CardType.Attack;

            config.TargetType = TargetType.RandomEnemy;

            config.Keywords = Keyword.Forbidden| Keyword.Replenish;
            config.UpgradedKeywords = Keyword.Forbidden | Keyword.Replenish;

            config.RelativeKeyword = Keyword.Exile;
            config.UpgradedRelativeKeyword = Keyword.Exile;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(MeteonicDebrisDef))]
    public sealed class MeteonicDebris : RNGCardPackCard
    {
        public MeteonicDebris() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinDamage = 4;
            BaseRandomMaxDamage = 8;

            BaseUpgradedRandomMinDamage = 6;
            BaseUpgradedRandomMaxDamage = 10;
        }

        public override IEnumerable<BattleAction> OnDraw()
        {
            return this.EnterHandReactor(true);
        }

        public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
        {
            if (dstZone != CardZone.Hand)
                return null;

            return this.EnterHandReactor(true);
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            if (base.Zone == CardZone.Hand)
                base.React(this.EnterHandReactor(true));
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            return this.EnterHandReactor(false);
        }

        private IEnumerable<BattleAction> EnterHandReactor(bool ensureInHand = true)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (ensureInHand && base.Zone != CardZone.Hand)
            {
                Debug.LogWarning(this.Name + " is not in hand.");
                yield break;
            }

            yield return RNGAttackAction(base.Battle.RandomAliveEnemy, RandomMinDamage, RandomMaxDamage, false, true);

            yield return new ExileCardAction(this);

            yield break;
        }
    }
}
