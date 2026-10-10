using System.Linq;
using LBoL.ConfigData;
using RNGCardPack.Source.Patches;

namespace RNGCardPack.Source.Packs
{
    public sealed class RNGCardPackDef : RNGCardPackTemplate
    {
        public new PackConfig MakeConfig()
        {
            PackConfig config = GetDefaultPackConfig();
            config.Id = GetId();
            config.CardList = RNGCardGameEventManager.GetList("RNGCards").ToList();
            return config;
        }
    }
}