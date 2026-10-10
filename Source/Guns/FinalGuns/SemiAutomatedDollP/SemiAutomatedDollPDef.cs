using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Guns.FinalGuns.SemiAutomatedDollP
{
    public sealed class SemiAutomatedDollPDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777010;

            config.Name = "SemiAutomatedDollP";

            return config;
        }
    }
}
