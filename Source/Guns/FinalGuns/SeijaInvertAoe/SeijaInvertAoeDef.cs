using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;

namespace RNGCardPack.Source.Guns.FinalGuns.SeijaInvertAoe
{
    public sealed class SeijaInvertAoeDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777001;

            config.Name = "SeijaInvertAoe";

            return config;
        }
    }
}
