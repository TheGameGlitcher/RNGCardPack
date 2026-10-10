using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using RNGCardPack.Source.StatusEffects.Template;

namespace RNGCardPack.Source.StatusEffects.Dummies
{
    public sealed class DummyUnpredictableSeDef : RNGCardPackStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(DummyUnpredictableSeDef))]
    public sealed class DummyUnpredictableSe : RNGCardPackStatusEffect
    {
    }
}