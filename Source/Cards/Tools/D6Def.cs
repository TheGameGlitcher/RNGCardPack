using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RNGCardPack.Source.Cards.Tools
{
    public sealed class D6Def : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.IsPooled = false;

            config.Colors = new ManaColor[] { ManaColor.Colorless };

            config.Cost = new ManaGroup() { };

            config.IsUpgradable = false;

            config.Rarity = Rarity.Rare;

            config.Value1 = 1;

            config.Value2 = 1;

            config.ToolPlayableTimes = 1;

            config.Type = CardType.Tool;

            config.TargetType = TargetType.Nobody;

            config.Illustrator = "KrystalFlamingo";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(D6Def))]
    public sealed class D6 : RNGCardPackCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            else
            {
                Exhibit[] exhibits = base.Battle.Player.Exhibits.Where((Exhibit e) => e.LosableType == ExhibitLosableType.Losable).ToArray();
                Exhibit newExhibit;
                List<Exhibit> exhibitsToLose = new List<Exhibit>();

                for (int i = 0; i < Value1; i++)
                    exhibitsToLose.Add(exhibits.Sample(base.GameRun.BattleCardRng));

                foreach (Exhibit exhibit in exhibitsToLose)
                    base.GameRun.LoseExhibit(exhibit, false, true);

                for (int i = 0; i < Value2; i++)
                {
                    newExhibit = GameRun.RollNormalExhibit(GameRun.ExhibitRng, new ExhibitWeightTable(new RarityWeightTable(0.5f, 0.33f, 0.17f, 0f), AppearanceWeightTable.AllOnes), null);

                    while (GameRun.Player.HasExhibit(newExhibit))
                        newExhibit = GameRun.RollNormalExhibit(GameRun.ExhibitRng, new ExhibitWeightTable(new RarityWeightTable(0.5f, 0.33f, 0.17f, 0f), AppearanceWeightTable.AllOnes), null);

                    if (newExhibit != null)
                        GameMaster.DebugGainExhibit(newExhibit);
                }

                    yield break;
            }
        }
    }
}
