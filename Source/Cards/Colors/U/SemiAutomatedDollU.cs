using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.U
{
    public sealed class SemiAutomatedDollUDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.FindInBattle = false;

            config.Colors = new ManaColor[] { ManaColor.Blue };

            config.Cost = new ManaGroup() { };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

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

    [EntityLogic(typeof(SemiAutomatedDollUDef))]
    public sealed class SemiAutomatedDollU : SemiAutomatedDollBase
    {
        public SemiAutomatedDollU() : base()
        {
            IsRandom = true;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;

            BasePercentValue2 = 25;
            BaseUpgradedPercentValue2 = 25;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
            base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffled, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
        }

        private IEnumerable<BattleAction> TransformCard(GameEventArgs args)
        {
            var action = new RollPercentAction(base.Battle.Player, PercentValue1, true);

            yield return action;

            Card card = Library.CreateCard((action.Args.Result) ? PhilosopherColor : GenericColors.Sample(base.BattleRng), IsUpgraded);

            yield return new TransformCardAction(this, card);

            yield break;
        }

        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            if (base.Battle.BattleShouldEnd)
                return null;

            return TurnEndingActions();
        }

        private IEnumerable<BattleAction> TurnEndingActions()
        {
            this.NotifyActivating();

            List<Card> cards = new List<Card>();

            for (int i = 0; i < Value1; i++)
            {
                Card card = Library.CreateCard(TokenCards.Sample(base.BattleRng));

                var action = new RollPercentAction(base.Battle.Player, PercentValue2, true);

                yield return action;

                if (card.CanUpgradeAndPositive && action.Args.Result)
                    card.Upgrade();

                cards.Add(card);
            }

            yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);

            yield break;
        }
    }
}