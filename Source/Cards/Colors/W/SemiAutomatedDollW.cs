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
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class SemiAutomatedDollWDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.FindInBattle = false;

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;

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

    [EntityLogic(typeof(SemiAutomatedDollWDef))]
    public sealed class SemiAutomatedDollW : SemiAutomatedDollBase
    {
        public SemiAutomatedDollW() : base()
        {
            IsRandom = true;

            BaseRandomMinShield = 10;
            BaseRandomMaxShield = 20;

            BaseUpgradedRandomMinShield = 15;
            BaseUpgradedRandomMaxShield = 25;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;

            BasePercentValue2 = 30;
            BaseUpgradedPercentValue2 = 45;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
            base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffled, new EventSequencedReactor<GameEventArgs>(this.TransformCard));

            base.ReactBattleEvent<DamageEventArgs>(base.Battle.Player.DamageReceiving, new EventSequencedReactor<DamageEventArgs>(this.OnDamageRecieving));
        }

        private IEnumerable<BattleAction> TransformCard(GameEventArgs args)
        {
            Card card = Library.CreateCard((RNGGenericPercent(PercentValue1, true, true)) ? PhilosopherColor : GenericColors.Sample(base.BattleRng), IsUpgraded);

            yield return new TransformCardAction(this, card);

            yield break;
        }

        private IEnumerable<BattleAction> OnDamageRecieving(DamageEventArgs args)
        {
            if (args.Cause != ActionCause.OnlyCalculate && base.Battle.AllAliveEnemies.Contains(args.ActionSource))
            {
                this.NotifyActivating();

                yield return RNGBlockShieldAction(RandomMinShield, RandomMaxShield, true);
            }

            yield break;
        }

        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            if (base.Battle.BattleShouldEnd && !RNGGenericPercent(PercentValue2, true, true))
                return null;

            return TurnEndingActions();
        }

        private IEnumerable<BattleAction> TurnEndingActions()
        {
            List<Card> cards = (from hand in base.Battle.HandZone
                                where hand.CanUpgradeAndPositive
                                select hand).ToList<Card>();

            if (cards.Count <= Value1)
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

            yield break;
        }
    }
}