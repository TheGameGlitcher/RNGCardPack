using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Guns.FinalGuns.SeijaInvertAoe
{
    public sealed class SeijaInvertAoeBDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777002;

            config.Name = "SeijaInvertAoeB";

            return config;
        }
    }
}
