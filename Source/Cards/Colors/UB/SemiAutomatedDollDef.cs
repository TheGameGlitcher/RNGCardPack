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
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.UB
{
    public sealed class SemiAutomatedDollDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Blue, ManaColor.Black };

            config.Cost = new ManaGroup() { Hybrid = 1, HybridColor = 4 };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Nobody;

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

    [EntityLogic(typeof(SemiAutomatedDollDef))]
    public sealed class SemiAutomatedDoll : SemiAutomatedDollBase
    {
        public SemiAutomatedDoll() : base()
        {
            IsRandom = true;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
            base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffled, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
        }

        private IEnumerable<BattleAction> TransformCard(GameEventArgs args)
        {
            Card card = Library.CreateCard((RNGGenericPercent(PercentValue1, true)) ? PhilosopherColor : GenericColors.Sample(base.BattleRng), IsUpgraded);

            yield return new TransformCardAction(this, card);

            yield break;
        }
    }
}