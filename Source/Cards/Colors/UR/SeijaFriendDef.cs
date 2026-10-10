using HarmonyLib;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using static Spine.Unity.SpineAttachment;

namespace RNGCardPack.Source.Cards.Colors.UR
{
    public sealed class SeijaFriendDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.GunName = GunNameID.GetGunFromId(777001);      // SeijaInvertAoe
            config.GunNameBurst = GunNameID.GetGunFromId(777002); // SeijaInvertAoeB

            config.Type = CardType.Friend;

            config.TargetType = TargetType.AllEnemies;

            config.Colors = new ManaColor[] { ManaColor.Blue, ManaColor.Red };

            config.Cost = new ManaGroup() { Blue = 1, Red = 1, Any = 1 };

            config.Rarity = Rarity.Rare;

            config.Loyalty = 2;
            config.UpgradedLoyalty = 4;

            config.PassiveCost = 1;

            config.UltimateCost = -6;

            config.Value1 = 5;

            config.Value2 = 1;

            config.RelativeKeyword = Keyword.Upgrade;
            config.UpgradedRelativeKeyword = Keyword.Upgrade;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe", "ChaosSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe", "ChaosSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(SeijaFriendDef))]
    public sealed class SeijaFriend : RNGCardPackCard
    {
        public SeijaFriend() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BasePercentValue1 = 25;
            BaseUpgradedPercentValue1 = 50;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
        }

        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            return this.GetPassiveActions();
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (IsUpgraded)
                BaseUpgradedRandomMaxDamage += args.Card.Cost.Amount;

            else
                BaseRandomMaxDamage += args.Card.Cost.Amount;

            NotifyChanged();
        }

        public override IEnumerable<BattleAction> GetPassiveActions()
        {
            if (!base.Summoned || base.Battle.BattleShouldEnd)
                yield break;

            base.NotifyActivating();
            base.Loyalty += base.PassiveCost;

            for (int i = 0; i < base.Battle.FriendPassiveTimes && !Battle.BattleShouldEnd; i++)
            {
                yield return PerformAction.Sfx("FairySupport", 0f);

                yield return RNGAttackAction(base.Battle.AllAliveEnemies, RandomMinDamage, RandomMaxDamage, true);
            }

            if (IsUpgraded)
                BaseUpgradedRandomMaxDamage = 0;

            else
                BaseRandomMaxDamage = 0;

            NotifyChanged();

            yield return new SpecialResolveAction(this);

            yield break;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Ultimate)
            {
                base.Loyalty += base.UltimateCost;
                base.UltimateUsed = true;

                yield return SkillAnime;

                yield return BuffAction<ReverseHierarchySe>(Value2, 0, 0, PercentValue1);
            }

            yield break;
        }
    }
}
