using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.ImageLoader;
using RNGCardPack.Source.Localization;
using UnityEngine;

namespace RNGCardPack.Source.StatusEffects.Template
{
    public class RNGCardPackStatusEffectTemplate : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return RNGCardPackDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return RNGCardPackLocalization.StatusEffectsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return RNGCardPackImageLoader.LoadStatusEffectLoader(status: this);
        }

        public override StatusEffectConfig MakeConfig()
        {
            return GetDefaultStatusEffectConfig();
        }

        public static StatusEffectConfig GetDefaultStatusEffectConfig()
        {
            return RNGCardPackDefaultConfig.DefaultStatusEffectConfig();
        }
    }
}