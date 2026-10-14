using HarmonyLib;

namespace RNGCardPack
{
    public static class PInfo
    {
        public const string GUID = "thegameglitcher.LBoL.cardpack.RNGCardPack";
        public const string Name = "RNGCardPack";
        public const string version = "1.0.1";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
