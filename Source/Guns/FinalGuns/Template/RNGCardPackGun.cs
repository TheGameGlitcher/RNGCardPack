using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using RNGCardPack.Source.Config;

namespace RNGCardPack.Source.Guns.FinalGuns.Template
{
    public class RNGCardPackGun : GunTemplate
    {
        public override IdContainer GetId()
        {
            return MakeConfig().Name;
        }

        public override GunConfig MakeConfig()
        {
            return RNGCardPackDefaultConfig.DefaultGunConfig();
        }
    }
}
