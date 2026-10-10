using System;
using System.Collections.Generic;
using HarmonyLib;
using LBoL.Core;
using RNGCardPack.Source.Cards.Colors.B;
using RNGCardPack.Source.Cards.Colors.G;
using RNGCardPack.Source.Cards.Colors.GB;
using RNGCardPack.Source.Cards.Colors.R;
using RNGCardPack.Source.Cards.Colors.BRG;
using RNGCardPack.Source.Cards.Colors.U;
using RNGCardPack.Source.Cards.Colors.W;
using RNGCardPack.Source.Cards.Developer;
using RNGCardPack.Source.Cards.Tools;
using RNGCardPack.Source.Cards.Colors.WR;
using RNGCardPack.Source.Cards.Colors.UR;
using RNGCardPack.Source.Cards.Colors.WU;
using RNGCardPack.Source.Cards.Colors.UB;
using RNGCardPack.Source.Cards.Colors.C;
using RNGCardPack.Source.Cards.Colors.P;

namespace RNGCardPack.Source.Patches
{
    [HarmonyPatch]
    internal class RNGCardGameEventManager
    {
        public static IEnumerable<string> GetList(string type)
        {
            switch (type)
            {
                case "RNGCards":
                    return RNGCards;
                default:
                    break;
            }

            return new List<string>();
        }

        public static readonly List<string> RNGCards = new List<string>
            {
                nameof(RandomAttackTest),
                nameof(RandomBlockTest),
                nameof(RandomDrawTest),
                nameof(ChaosStatusTest),
                nameof(LuckStatusTest),
                nameof(RandomShieldTest),
                nameof(StrangeDiceBag),
                nameof(D8),
                nameof(D20),
                nameof(D100),
                nameof(D6),
                nameof(FragmentedGrimoire),
                nameof(AmanojakusGambit),
                nameof(PoisonNail),
                nameof(BurstShot),
                nameof(UnstableBarrier),
                nameof(ManaConverter),
                nameof(WhiteRabbitLuck),
                nameof(StrangeElixir),
                nameof(OpticalCalibration),
                nameof(PhantomicKiller),
                nameof(CelestialsBlessing),
                nameof(BlackMarketKaleidoscope),
                nameof(UncertainFuture),
                nameof(AstrologicalDivination),
                nameof(SpacetimeSeam),
                nameof(MeteonicShower),
                nameof(MeteonicDebris),
                nameof(InfluenceOfTheEast),
                nameof(NervePoison),
                nameof(LuckyClover),
                nameof(RecursiveShot),
                nameof(SeijaFriend),
                nameof(CirnosPlan),
                nameof(PovertyGodsWrath),
                nameof(YamaDecree),
                nameof(HonorDefianceAvoidance),
                nameof(SemiAutomatedDoll),
                nameof(SemiAutomatedDollW),
                nameof(SemiAutomatedDollU),
                nameof(SemiAutomatedDollB),
                nameof(SemiAutomatedDollR),
                nameof(SemiAutomatedDollG),
                nameof(SemiAutomatedDollC),
                nameof(SemiAutomatedDollP)
            };
    }
}