using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Koishi;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class SpacetimeSeamDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            config.Keywords = Keyword.Forbidden;
            config.UpgradedKeywords = Keyword.Forbidden;

            config.RelativeKeyword = Keyword.Exile;
            config.UpgradedRelativeKeyword = Keyword.Exile;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(SpacetimeSeamDef))]
    public sealed class SpacetimeSeam : RNGCardPackCard
    {
        public SpacetimeSeam() : base()
        {
            IsRandom = true;

            BasePercentValue1 = 5;
            BaseUpgradedPercentValue1 = 10;

            BaseUpgradedPercentValue2 = 10;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }

        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (this == base.Battle.EnumerateAllCards().FirstOrDefault((Card card) => card is SpacetimeSeam))
            {
                List<Card> list = (from card in base.Battle.DrawZone
                                   where card is SpacetimeSeam
                                   select card).ToList<Card>();

                List<Card> upgradedList = (from card in base.Battle.DrawZone
                                           where card is SpacetimeSeam && card.IsUpgraded
                                           select card).ToList<Card>();

                int level = list.Sum((Card card) => card.Value1);

                yield return new ExileManyCardAction(list);

                yield return BuffAction<SpacetimeSeamSe>(level, 0, 0, (upgradedList.Count > 0) ? BaseUpgradedPercentValue1 : BasePercentValue1);
            }

            yield break;
        }
    }
}
