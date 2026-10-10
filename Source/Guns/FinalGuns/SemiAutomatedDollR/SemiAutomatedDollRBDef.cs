using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.FinalGuns.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.Guns.FinalGuns.SemiAutomatedDollR
{
    public sealed class SemiAutomatedDollRBDef : RNGCardPackGun
    {
        public override GunConfig MakeConfig()
        {
            GunConfig config = RNGCardPackDefaultConfig.DefaultGunConfig();

            config.Id = 777009;

            config.Name = "SemiAutomatedDollRB";

            return config;
        }
    }
}
