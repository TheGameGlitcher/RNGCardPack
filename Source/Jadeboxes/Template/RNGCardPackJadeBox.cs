using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Localization;

namespace RNGCardPack.Source.Jadeboxes.Template
{
    public class RNGCardPackJadeBox : JadeBoxTemplate
    {
        public override IdContainer GetId()
        {
            return RNGCardPackDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return RNGCardPackLocalization.JadeBoxBatchLoc.AddEntity(this);
        }

        public override JadeBoxConfig MakeConfig()
        {
            return GetDefaultJadeBoxConfig();
        }

        public JadeBoxConfig GetDefaultJadeBoxConfig()
        {
            return RNGCardPackDefaultConfig.DefaultJadeBoxConfig();
        }

    }
}