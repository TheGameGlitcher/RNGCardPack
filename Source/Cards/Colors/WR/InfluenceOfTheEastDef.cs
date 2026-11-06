using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RNGCardPack.Source.Cards.Colors.WR
{
    public sealed class InfluenceOfTheEastDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.Red, ManaColor.White };

            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;

            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            config.Keywords = Keyword.Forbidden;
            config.UpgradedKeywords = Keyword.Forbidden | Keyword.Shield;

            config.RelativeKeyword = Keyword.Exile;
            config.UpgradedRelativeKeyword = Keyword.Exile;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe", "Reflect" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe", "Reflect" };

            config.Illustrator = "Radal";

            config.SubIllustrator = new List<string>() { "Radal(Old)" };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(InfluenceOfTheEastDef))]
    public sealed class InfluenceOfTheEast : RNGCardPackCard
    {
        public InfluenceOfTheEast() : base()
        {
            IsRandom = true;

            BaseRandomMinValue1 = 10;
            BaseRandomMaxValue1 = 20;

            BaseUpgradedRandomMinValue1 = 10;
            BaseUpgradedRandomMaxValue1 = 20;

            BaseUpgradedRandomMinShield = 5;
            BaseUpgradedRandomMaxShield = 15;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }

        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (this == base.Battle.EnumerateAllCards().FirstOrDefault((Card card) => card is InfluenceOfTheEast))
            {
                int reflection = 0;
                int barrier = 0;

                List<Card> cards = (from card in base.Battle.DrawZone
                                   where card is InfluenceOfTheEast
                                   select card).ToList<Card>();

                yield return new ExileManyCardAction(cards);

                foreach (RNGCardPackCard card in cards.Cast<RNGCardPackCard>())
                {
                    var action1 = new RollRandomRangeAction(base.Battle.Player, card.RandomMinValue1, card.RandomMaxValue1);

                    yield return action1;

                    reflection += action1.Args.Result;

                    if (card.IsUpgraded)
                    {
                        var action2 = new RollRandomRangeAction(base.Battle.Player, card.RandomMinShield, card.RandomMaxShield);

                        yield return action2;

                        barrier += action2.Args.Result;
                    }
                }

                yield return BuffAction<Reflect>(reflection);

                if (base.Battle.Player.HasStatusEffect<Reflect>())
                    base.Battle.Player.GetStatusEffect<Reflect>().Gun = (this.IsUpgraded ? GunNameID.GetGunFromId(21501) : GunNameID.GetGunFromId(21500));

                if (barrier > 0)
                    yield return new CastBlockShieldAction(base.Battle.Player, 0, barrier);
            }

            yield break;
        }
    }
}
