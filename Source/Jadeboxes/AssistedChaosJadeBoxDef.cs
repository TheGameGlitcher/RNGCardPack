using HarmonyLib;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.EntityLib.Adventures;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.Jadeboxes.Template;
using RNGCardPack.Source.Packs;
using RNGCardPack.Source.Patches;
using RNGCardPack.Source.StatusEffects.Effects;
using System.Collections.Generic;
using System.Linq;


namespace RNGCardPack.Source.Jadeboxes
{
    public sealed class AssistedChaosJadeBoxDef : RNGCardPackJadeBox
    {
        public override JadeBoxConfig MakeConfig()
        {
            JadeBoxConfig config = DefaultConfig();

            config.Value1 = 2;

            config.Value2 = 2;

            return config;
        }

        [EntityLogic(typeof(AssistedChaosJadeBoxDef))]
        public sealed class AssistedChaosJadeBox : JadeBox
        {
            protected override void OnGain(GameRunController gameRun)
            {
                Card[] cards = gameRun.RollCards(gameRun.CardRng, new CardWeightTable(RarityWeightTable.EnemyCard, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot, false), Value1, false, false, (CardConfig config) => config.RelativeEffects.Contains("DummyUnpredictableSe"));

                gameRun.AddDeckCards(cards, false, null);
            }

            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStated));
            }

            private IEnumerable<BattleAction> OnBattleStated(GameEventArgs args)
            {
                yield return new ApplyStatusEffectAction<TempChaosSe>(base.Battle.Player, Value2);

                yield break;
            }
        }
    }
}
