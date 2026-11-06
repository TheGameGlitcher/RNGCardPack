using HarmonyLib;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using RNGCardPack.Source.BattleActions.RandomApplyStatusEffectAction;
using RNGCardPack.Source.BattleActions.RandomCastBlockShieldAction;
using RNGCardPack.Source.BattleActions.RandomDamageAction;
using RNGCardPack.Source.BattleActions.RandomDrawManyCardAction;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using RNGCardPack.Source.BattleActions.RollPercentAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.EventsManager
{
    [HarmonyPatch]
    public class RNGCardEventsManager
    {
        public static GameEvent<PercentEventArgs> PercentRolling { get; set; }
        public static GameEvent<PercentEventArgs> PercentRolled { get; set; }

        public static GameEvent<RandomRangeEventArgs> RangeRolling { get; set; }
        public static GameEvent<RandomRangeEventArgs> RangeRolled { get; set; }

        public static GameEvent<RandomBlockShieldEventArgs> BlockShieldRangeCasting { get; set; }
        public static GameEvent<RandomBlockShieldEventArgs> BlockShieldRangeCasted { get; set; }

        public static GameEvent<RandomBlockShieldEventArgs> BlockShieldRangeGaining { get; set; }
        public static GameEvent<RandomBlockShieldEventArgs> BlockShieldRangeGainined { get; set; }

        public static GameEvent<RandomDamageEventArgs> DamageRangeDealing { get; set; }
        public static GameEvent<RandomDamageEventArgs> DamageRangeDealt { get; set; }

        public static GameEvent<RandomCardEventArgs> CardRangeDrawing { get; set; }
        public static GameEvent<RandomCardEventArgs> CardRangeDrawn { get; set; }

        public static GameEvent<RandomStatusEffectApplyEventArgs> StatusEffectRangeApplying { get; set; }
        public static GameEvent<RandomStatusEffectApplyEventArgs> StatusEffectRangeApplied { get; set; }

        [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.EnterBattle))]
        private static bool Prefix(GameRunController __instance)
        {
            PercentRolling = new GameEvent<PercentEventArgs>();
            PercentRolled = new GameEvent<PercentEventArgs>();

            RangeRolling = new GameEvent<RandomRangeEventArgs>();
            RangeRolled = new GameEvent<RandomRangeEventArgs>();

            BlockShieldRangeCasting = new GameEvent<RandomBlockShieldEventArgs>();
            BlockShieldRangeCasted = new GameEvent<RandomBlockShieldEventArgs>();

            BlockShieldRangeGaining = new GameEvent<RandomBlockShieldEventArgs>();
            BlockShieldRangeGainined = new GameEvent<RandomBlockShieldEventArgs>();

            DamageRangeDealing = new GameEvent<RandomDamageEventArgs>();
            DamageRangeDealt = new GameEvent<RandomDamageEventArgs>();

            CardRangeDrawing = new GameEvent<RandomCardEventArgs>();
            CardRangeDrawn = new GameEvent<RandomCardEventArgs>();

            StatusEffectRangeApplying = new GameEvent<RandomStatusEffectApplyEventArgs>();
            StatusEffectRangeApplied = new GameEvent<RandomStatusEffectApplyEventArgs>();

            return true;
        }
    }
}
