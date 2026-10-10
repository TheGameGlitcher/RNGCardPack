using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.StatusEffects.Template;
using System.Collections.Generic;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class TempLuckNegativeSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Type = StatusEffectType.Negative;

            config.RelativeEffects = new List<string> { "LuckNegativeSe" };

            config.VFX = "DebuffGreen";

            return config;
        }
    }

    [EntityLogic(typeof(TempLuckNegativeSeDef))]
    public sealed class TempLuckNegativeSe : RNGCardPackStatusEffect, IOpposing<TempLuckSe>
    {
        public OpposeResult Oppose(TempLuckSe other)
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
        }
    }
}