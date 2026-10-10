using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Guns.FinalGuns.MeteonicDebris
{
    public sealed class MeteonicDebrisDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777006;

            config.Name = "MeteonicDebris";

            return config;
        }
    }
}
