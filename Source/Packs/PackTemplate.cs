using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.ImageLoader;
using RNGCardPack.Source.Localization;

namespace RNGCardPack.Source.Packs
{
    public class RNGCardPackTemplate : PackTemplate
    {
        public override IdContainer GetId()
        {
            return RNGCardPackDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return RNGCardPackLocalization.PacksBatchLoc.AddEntity(this);
        }

        public override PackIcons LoadPackIcon()
        {
            return RNGCardPackImageLoader.LoadPackIconLoader(this);
        }

        public new PackConfig MakeConfig()
        {
            return GetDefaultPackConfig();
        }

        public static PackConfig GetDefaultPackConfig()
        {
            return RNGCardPackDefaultConfig.DefaultPackConfig();
        }
    }
}