using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.ImageLoader;
using RNGCardPack.Source.Localization;


namespace RNGCardPack.Source.Cards.Template
{
    public abstract class RNGCardPackCardTemplate : CardTemplate
    {
        public override IdContainer GetId()
        {
            return RNGCardPackDefaultConfig.DefaultID(this);
        }

        public override CardImages LoadCardImages()
        {
            return RNGCardPackImageLoader.LoadCardImages(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return RNGCardPackLocalization.CardsBatchLoc.AddEntity(this);
        }

        public CardConfig GetDefaultCardConfig()
        {
            return RNGCardPackDefaultConfig.DefaultCardConfig();
        }
    }


}


