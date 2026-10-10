using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.StatusEffects.Template;
using System.Collections.Generic;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class TempLuckSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string> { "LuckSe" };

            config.SFX = "Buff";

            config.VFX = "BuffGreen";

            return config;
        }
    }

    [EntityLogic(typeof(TempLuckSeDef))]
    public sealed class TempLuckSe : RNGCardPackStatusEffect, IOpposing<TempLuckNegativeSe>
    {
        public OpposeResult Oppose(TempLuckNegativeSe other)
        {
            if (base.Level < other.Level)
            {
                other.Level -= base.Level;

                return OpposeResult.KeepOther;
            }

            if (base.Level == other.Level)
                return OpposeResult.Neutralize;

            base.Level -= other.Level;

            return OpposeResult.KeepSelf;
        }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is PlayerUnit))
                Debug.LogError(this.DebugName + " should not apply to non-player unit.");

            base.HandleOwnerEvent<UnitEventArgs>(unit.TurnEnded, delegate (UnitEventArgs _) { this.React(new RemoveStatusEffectAction(this, true, 0.1f)); });
        }
    }
}