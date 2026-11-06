using HarmonyLib;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using RNGCardPack.Source.Structs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using static LBoL.Core.Cards.Card;

namespace RNGCardPack.Source.Patches
{
    [HarmonyPatch]
    internal class RangeFormattingPatch
    {
        private static readonly FieldInfo CardField = typeof(CardFormatWrapper).GetField("_card", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch(typeof(CardFormatWrapper), "FormatArgument")]
        private static bool Prefix(object __instance, object arg, ref string format, ref string __result)
        {
            if (arg is BlockRangeInfo blockRangeInfo)
            {
                var _card = (Card)CardField.GetValue(__instance);

                int minBlock = blockRangeInfo.MinBlock;
                int maxBlock = blockRangeInfo.MaxBlock;

                BattleController battle = _card.Battle;

                if (battle == null)
                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minBlock, minBlock, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxBlock, maxBlock, format)}";

                else
                {
                    int scaledMinBlock = battle.CalculateBlockShield(_card, (float)minBlock, 0f, BlockShieldType.Normal).block;

                    int scaledMaxBlock = battle.CalculateBlockShield(_card, (float)maxBlock, 0f, BlockShieldType.Normal).block;

                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minBlock, scaledMinBlock, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxBlock, scaledMaxBlock, format)}";
                }

                return false;
            }

            else if (arg is DamageRangeInfo damageRangeInfo)
            {
                var _card = (Card)CardField.GetValue(__instance);

                int minDamage = damageRangeInfo.MinDamage;
                int maxDamage = damageRangeInfo.MaxDamage;

                BattleController battle = _card.Battle;

                if (battle == null)
                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minDamage, minDamage, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxDamage, maxDamage, format)}";

                else
                {
                    int scaledMinDamage = battle.CalculateDamage(_card, battle.Player, _card.PendingTarget, new DamageInfo(minDamage, damageRangeInfo.Type, false, damageRangeInfo.IsAccuracy));

                    int scaledMaxDamage = battle.CalculateDamage(_card, battle.Player, _card.PendingTarget, new DamageInfo(maxDamage, damageRangeInfo.Type, false, damageRangeInfo.IsAccuracy));

                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minDamage, scaledMinDamage, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxDamage, scaledMaxDamage, format)}";
                }

                return false;
            }

            else if (arg is ShieldRangeInfo shieldRangeInfo)
            {
                var _card = (Card)CardField.GetValue(__instance);

                int minShield = shieldRangeInfo.MinShield;
                int maxShield = shieldRangeInfo.MaxShield;

                BattleController battle = _card.Battle;

                if (battle == null)
                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minShield, minShield, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxShield, maxShield, format)}";

                else
                {
                    int scaledMinShield = battle.CalculateBlockShield(_card, (float)minShield, 0f, BlockShieldType.Normal).block;

                    int scaledMaxShield = battle.CalculateBlockShield(_card, (float)maxShield, 0f, BlockShieldType.Normal).block;

                    __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(minShield, scaledMinShield, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(maxShield, scaledMaxShield, format)}";
                }

                return false;
            }

            else if (arg is GenericRangeInfo genericRangeInfo)
            {
                int min = genericRangeInfo.Min;
                int max = genericRangeInfo.Max;

                __result = $"{GameEntityFormatWrapper.WrappedFormatNumber(min, min, format)}~{GameEntityFormatWrapper.WrappedFormatNumber(max, max, format)}";

                return false;
            }

            else
                return true;
        }
    }
}
