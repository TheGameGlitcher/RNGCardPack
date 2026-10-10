using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.Cards.Colors.B
{
    public sealed class NervePoisonDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Black };

            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.RandomEnemy;

            config.Keywords = Keyword.Forbidden;
            config.UpgradedKeywords = Keyword.Forbidden;

            config.RelativeKeyword = Keyword.Exile;
            config.UpgradedRelativeKeyword = Keyword.Exile;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe", "Weak", "Fragil" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe", "Weak", "Fragil" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(NervePoisonDef))]
    public sealed class NervePoison : RNGCardPackCard
    {
        public NervePoison() : base()
        {
            IsRandom = true;

            BaseRandomMinValue1 = 1;
            BaseRandomMaxValue1 = 3;

            BaseUpgradedRandomMinValue1 = 1;
            BaseUpgradedRandomMaxValue1 = 3;

            BasePercentValue1 = 25;
            BaseUpgradedPercentValue1 = 35;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }

        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (this == base.Battle.EnumerateAllCards().FirstOrDefault((Card card) => card is NervePoison))
            {
                int weakLevel = 0;

                List<Card> cards = (from card in base.Battle.DrawZone
                                    where card is NervePoison
                                    select card).ToList<Card>();

                yield return new ExileManyCardAction(cards);

                foreach (RNGCardPackCard card in cards.Cast<RNGCardPackCard>())
                {
                    weakLevel = RNGGenericRange(RandomMinValue1, RandomMaxValue1, true);

                    if (RNGGenericPercent(PercentValue1, true))
                    {
                        foreach (EnemyUnit enemy in base.Battle.AllAliveEnemies)
                            yield return new ApplyStatusEffectAction<Weak>(enemy, 0, weakLevel * 2, 0, 0, 0.2f);
                    }

                    else
                        yield return new ApplyStatusEffectAction<Weak>(base.Battle.RandomAliveEnemy, 0 , weakLevel, 0, 0, 0.2f);
                }
            }

            yield break;
        }
    }
}
