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
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.InputSystem.Controls;

namespace RNGCardPack.Source.Cards.Colors.W
{
    public sealed class UnstableBarrierDef : RNGCardPackCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetDefaultCardConfig();

            config.Colors = new ManaColor[] { ManaColor.White };

            config.Cost = new ManaGroup() { White = 1, Any = 1 };

            config.Rarity = Rarity.Common;

            config.Block = 0;

            config.Shield = 0;

            config.Type = CardType.Defense;

            config.TargetType = TargetType.Self;

            config.RelativeKeyword = Keyword.Block;
            config.UpgradedRelativeKeyword = Keyword.Block | Keyword.Shield;

            config.RelativeEffects = new List<string> { "DummyUnpredictableSe" };
            config.UpgradedRelativeEffects = new List<string> { "DummyUnpredictableSe" };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(UnstableBarrierDef))]
    public sealed class UnstableBarrier : RNGCardPackCard
    {
        public UnstableBarrier() : base()
        {
            IsRandom = true;
            AllowOrderChaos = true;

            BaseRandomMinBlock = 10;
            BaseRandomMaxBlock = 20;

            BaseUpgradedRandomMinBlock = 15;
            BaseUpgradedRandomMaxBlock = 25;

            BaseUpgradedPercentValue1 = 25;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (IsUpgraded && RNGGenericPercent(PercentValue1, true))
            {
                int blockResult = RNGGenericRange(RandomMinBlock.Block, RandomMaxBlock.Block, true);

                yield return new CastBlockShieldAction(base.Battle.Player, blockResult / 2, blockResult / 2);
            }

            else
                yield return RNGBlockShieldAction(RandomMinBlock, RandomMaxBlock, true);

            yield break;
        }
    }
}