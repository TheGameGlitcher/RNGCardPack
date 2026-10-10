using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.CustomKeywords;
using RNGCardPack.Source.StatusEffects.Template;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Effects
{
    public sealed class ChaosSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.SFX = "LunaticTorch";

            return config;
        }
    }

    [EntityLogic(typeof(ChaosSeDef))]
    public sealed class ChaosSe : RNGCardPackStatusEffect, IOpposing<OrderSe>
    {
        public OpposeResult Oppose(OrderSe other)
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