using JetBrains.Annotations;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Utils;
using RNGCardPack.Source.BattleActions.EventsManager;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using RNGCardPack.Source.Cards.Statuses;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.StatusEffects.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class PovertyGodsWrathSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "Buff";

            return config;
        }
    }

    [EntityLogic(typeof(PovertyGodsWrathSeDef))]
    public sealed class PovertyGodsWrathSe : RNGCardPackStatusEffect
    {
        public PovertyGodsWrathSe() : base()
        {
            PercentValue1 = 50;
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            ReactOwnerEvent(RNGCardEventsManager.PercentRolled, OnPercentRolled);
        }

        private IEnumerable<BattleAction> OnPercentRolled(PercentEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
                yield break;

            if (args.Source != this && !args.IsReroll && !args.Result)
            {
                var action = new RollPercentAction(this, PercentValue1, false);

                yield return action;

                if (action.Args.Result)
                {
                    this.NotifyActivating();

                    yield return new ApplyStatusEffectAction<Firepower>(this.Owner, this.Level);
                }
            }

            yield break;
        }
    }
}