using HarmonyLib;

namespace RNGCardPack
{
    public static class PInfo
    {
        //Rename the variable below to prevent conflicts between mod.
        public const string GUID = "thegameglitcher.LBoL.cardpack.RNGCardPack";
        public const string Name = "RNGCardPack";
        public const string version = "1.0.0";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
