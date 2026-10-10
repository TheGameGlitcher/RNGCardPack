using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Guns.FinalGuns.MeteonicShower
{
    public sealed class MeteonicShowerBDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777005;

            config.Name = "MeteonicShowerB";

            return config;
        }
    }
}
