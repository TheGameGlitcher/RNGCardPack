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
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class SemiAutomatedDollBDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.FindInBattle = false;

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Cost = new ManaGroup() { };

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

    [EntityLogic(typeof(SemiAutomatedDollBDef))]
    public sealed class SemiAutomatedDollB : SemiAutomatedDollBase
    {
        public SemiAutomatedDollB() : base()
        {
            IsRandom = true;

            BaseRandomMinValue1 = 5;
            BaseRandomMaxValue1 = 10;

            BaseUpgradedRandomMinValue1 = 7;
            BaseUpgradedRandomMaxValue1 = 14;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
            base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffled, new EventSequencedReactor<GameEventArgs>(this.TransformCard));
        }

        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            if (base.Battle.BattleShouldEnd)
                return null;

            return TurnEndingActions();
        }

        private IEnumerable<BattleAction> TransformCard(GameEventArgs args)
        {
            Card card = Library.CreateCard((RNGGenericPercent(PercentValue1, true)) ? PhilosopherColor : GenericColors.Sample(base.BattleRng), IsUpgraded);

            yield return new TransformCardAction(this, card);

            yield break;
        }

        private IEnumerable<BattleAction> TurnEndingActions()
        {
            this.NotifyActivating();

            yield return new DamageAction(base.Battle.Player, base.Battle.RandomAliveEnemy, DamageInfo.HpLose(RNGGenericRange(RandomMinValue1, RandomMaxValue1, true)), GunNameID.GetGunFromId(862)); // Sacrifice

            yield break;
        }
    }
}