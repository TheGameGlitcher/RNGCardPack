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
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.P
{
    public sealed class SemiAutomatedDollPDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.FindInBattle = false;

            config.Colors = new ManaColor[] { ManaColor.White, ManaColor.Blue, ManaColor.Black, ManaColor.Red, ManaColor.Green, ManaColor.Colorless };

            config.Cost = new ManaGroup() { };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Damage = 0;

            config.GunName = GunNameID.GetGunFromId(777010);      // SemiAutomatedDollP
            config.GunNameBurst = GunNameID.GetGunFromId(777011); // SemiAutomatedDollPB

            config.Keywords = Keyword.Retain | Keyword.Exile;
            config.UpgradedKeywords = Keyword.Retain | Keyword.Exile;

            config.RelativeEffects = new List<string>() { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string>() { "DummyUnpredictableSe" };

            config.RelativeCards = new List<string>() { "SemiAutomatedDoll", "SemiAutomatedDollW", "SemiAutomatedDollU", "SemiAutomatedDollB", "SemiAutomatedDollR", "SemiAutomatedDollG", "SemiAutomatedDollC", "SemiAutomatedDollP" };
            config.UpgradedRelativeCards = new List<string>() { "SemiAutomatedDoll+", "SemiAutomatedDollW+", "SemiAutomatedDollU+", "SemiAutomatedDollB+", "SemiAutomatedDollR+", "SemiAutomatedDollG+", "SemiAutomatedDollC+", "SemiAutomatedDollP+" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(SemiAutomatedDollPDef))]
    public sealed class SemiAutomatedDollP : SemiAutomatedDollBase
    {
        public SemiAutomatedDollP() : base()
        {
            IsRandom = true;

            BaseRandomMinShield = 5;
            BaseRandomMaxShield = 15;

            BaseRandomMinDamage = 10;
            BaseRandomMaxDamage = 20;

            BaseUpgradedRandomMinDamage = 25;
            BaseUpgradedRandomMaxDamage = 30;

            BaseUpgradedRandomMinShield = 10;
            BaseUpgradedRandomMaxShield = 20;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;
        }

        private int BasePercentValueW1 { get; set; } = 30;
        private int BaseUpgradedPercentValueW1 { get; set; } = 45;
        public int PercentValueW1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedPercentValueW1;

                else
                    return BasePercentValueW1;
            }
        }

        private int BaseValueW1 { get; set; } = 1;
        private int BaseUpgradedValueW1 { get; set; } = 1;
        public int ValueW1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedValueW1;

                else
                    return BaseValueW1;
            }
        }

        private int BasePercentValueU1 { get; set; } = 30;
        private int BaseUpgradedPercentValueU1 { get; set; } = 45;
        public int PercentValueU1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedPercentValueU1;

                else
                    return BasePercentValueU1;
            }
        }

        private int BaseValueU1 { get; set; } = 2;
        private int BaseUpgradedValueU1 { get; set; } = 3;
        public int ValueU1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedValueU1;

                else
                    return BaseValueU1;
            }
        }

        private int BaseRandomMinValueB1 { get; set; } = 5;
        private int BaseUpgradedRandomMinValueB1 { get; set; } = 7;
        public int RandomMinValueB1
        {
            get
            {
                int finalValue = (IsUpgraded) ? BaseUpgradedRandomMinValueB1 : BaseRandomMinValueB1;

                finalValue -= ChaosLevel;

                if (finalValue >= MedianValueB1)
                    finalValue = MedianValueB1;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        private int BaseRandomMaxValueB1 { get; set; } = 10;
        private int BaseUpgradedRandomMaxValueB1 { get; set; } = 14;
        public int RandomMaxValueB1
        {
            get
            {
                int finalValue = (IsUpgraded) ? BaseUpgradedRandomMaxValueB1 : BaseRandomMaxValueB1;

                finalValue += ChaosLevel;

                if (finalValue <= MedianValueB1)
                    finalValue = MedianValueB1;

                if (AllowNegativeValues)
                    return finalValue;

                else
                {
                    if (finalValue < 0)
                        finalValue = 0;

                    return finalValue;
                }
            }
        }

        public int MedianValueB1
        {
            get
            {
                return (int)Math.Round(
                    (((IsUpgraded) ? (float)BaseUpgradedRandomMinValueB1 : (float)BaseRandomMinValueB1) +
                    ((IsUpgraded) ? (float)BaseUpgradedRandomMaxValueB1 : (float)BaseRandomMaxValueB1)) /
                    2);
            }
        }

        private int BaseValueG1 { get; set; } = 1;
        private int BaseUpgradedValueG1 { get; set; } = 2;
        public int ValueG1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedValueG1;

                else
                    return BaseValueG1;
            }
        }

        private int BasePercentValueC1 { get; set; } = 30;
        private int BaseUpgradedPercentValueC1 { get; set; } = 45;
        public int PercentValueC1
        {
            get
            {
                if (IsUpgraded)
                    return BaseUpgradedPercentValueC1;

                else
                    return BasePercentValueC1;
            }
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
            base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffled, new EventSequencedReactor<GameEventArgs>(this.TransformCard));

            base.HandleBattleEvent<DamageEventArgs>(base.Battle.Player.DamageReceiving, new GameEventHandler<DamageEventArgs>(this.OnDamageRecieving)); // Extra white effect
        }

        private IEnumerable<BattleAction> TransformCard(GameEventArgs args)
        {
            var action = new RollPercentAction(base.Battle.Player, PercentValue1, true);

            yield return action;

            Card card = Library.CreateCard((action.Args.Result) ? PhilosopherColor : GenericColors.Sample(base.BattleRng), IsUpgraded);

            yield return new TransformCardAction(this, card);

            yield break;
        }

        private void OnDamageRecieving(DamageEventArgs args)
        {
            if (base.Battle.HandZone.Contains(this) && args.Cause != ActionCause.OnlyCalculate && args.DamageInfo.DamageType == DamageType.Attack && base.Battle.AllAliveEnemies.Contains(args.Source))
            {
                this.NotifyActivating();

                React(new RandomCastBlockShieldAction(base.Battle.Player, base.Battle.Player, 0, 0, RandomMinShield, RandomMaxShield));
            }
        }

        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TunEndingActionsW())
                yield return action;

            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TurnEndingActionsU())
                yield return action;

            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TurnEndingActionsB())
                yield return action;

            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TurnEndingActionsR())
                yield return action;

            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TurnEndingActionsG())
                yield return action;

            if (base.Battle.BattleShouldEnd)
                yield break;

            foreach (BattleAction action in TurnEndingActionsC())
                yield return action;

            yield break;
        }

        private IEnumerable<BattleAction> TunEndingActionsW()
        {
            var action = new RollPercentAction(base.Battle.Player, PercentValueW1, true);

            yield return action;

            if (action.Args.Result)
            {
                List<Card> cards = (from hand in base.Battle.HandZone
                                    where hand.CanUpgradeAndPositive
                                    select hand).ToList<Card>();

                if (cards.Count <= ValueW1)
                {
                    this.NotifyActivating();

                    foreach (Card card in cards)
                        yield return new UpgradeCardAction(card);

                    yield break;
                }

                MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(cards) { Description = ExtraDescription1 };

                this.NotifyActivating();

                yield return new InteractionAction(interaction, false);

                yield return new UpgradeCardAction(interaction.SelectedCard);
            }

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActionsU()
        {
            this.NotifyActivating();

            List<Card> cards = new List<Card>();

            for (int i = 0; i < ValueU1; i++)
            {
                Card card = Library.CreateCard(TokenCards.Sample(base.BattleRng));

                var action = new RollPercentAction(base.Battle.Player, PercentValueU1, true);

                yield return action;

                if (card.CanUpgradeAndPositive && action.Args.Result)
                    card.Upgrade();

                cards.Add(card);
            }

            yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActionsB()
        {
            this.NotifyActivating();

            yield return new RandomDamageAction(base.Battle.Player, base.Battle.RandomAliveEnemy, RandomMinValueB1, RandomMaxValueB1, DamageType.HpLose, false, GunNameID.GetGunFromId(862), GunType.Single, true, AllowOrderChaos, AllowNegativeValues); // Sacrifice

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActionsR()
        {
            this.NotifyActivating();

            yield return new RandomDamageAction(base.Battle.Player, base.Battle.RandomAliveEnemy, RandomMinDamage, RandomMaxDamage, DamageType.Attack, false, GunName, GunType.Single, true, AllowOrderChaos, AllowNegativeValues);

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActionsG()
        {
            this.NotifyActivating();

            Type status = Statuses.Sample(base.BattleRng);

            yield return new ApplyStatusEffectAction(status, base.Battle.Player, ValueG1, ValueG1, ValueG1, null, 0.2f);

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActionsC()
        {
            this.NotifyActivating();

            var action = new RollPercentAction(base.Battle.Player, PercentValueC1, true);

            yield return action;

            Card card = Library.CreateCard((action.Args.Result) ? ToolGadgetCardsRare.Sample(base.BattleRng) : ToolGadgetCardsGeneric.Sample(base.BattleRng), false);

            if (IsUpgraded && card.CanUpgradeAndPositive)
                card.Upgrade();

            card.IsPlayTwiceToken = true;
            base.Battle.AddCardToFollowArea(card);

            yield return new PlayCardAction(card);

            yield break;
        }
    }
}