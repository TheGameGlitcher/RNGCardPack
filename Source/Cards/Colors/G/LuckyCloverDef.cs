using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.Cards.Colors.G
{
    public sealed class LuckyCloverDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Green };

            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.TargetType = TargetType.Self;

            config.Keywords = Keyword.Forbidden;
            config.UpgradedKeywords = Keyword.Forbidden;

            config.RelativeKeyword = Keyword.Exile;
            config.UpgradedRelativeKeyword = Keyword.Exile;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe", "TempLuckSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe", "TempLuckSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(LuckyCloverDef))]
    public sealed class LuckyClover : RNGCardPackCard
    {
        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }

        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (this == base.Battle.EnumerateAllCards().FirstOrDefault((Card card) => card is LuckyClover))
            {

                List<Card> cards = (from card in base.Battle.DrawZone
                                    where card is LuckyClover
                                    select card).ToList<Card>();

                int level = cards.Sum((Card card) => card.Value1);

                yield return new ExileManyCardAction(cards);

                yield return BuffAction<TempLuckSe>(level);
            }

            yield break;
        }
    }
}
