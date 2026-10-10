using HarmonyLib;
using LBoL.Core.Cards;
using LBoL.Presentation.UI.Panels;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RNGCardPack.Source.Patches
{
    [HarmonyPatch]
    internal class CardUiPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CardUi), nameof(CardUi.Awake));
        }

        private static void Postfix(CardUi __instance)
        {
            UiUtils.CardUiInstance = __instance;
        }
    }

    internal static class UiUtils
    {
        public static CardUi CardUiInstance { get; set; }

        public static void UpdateHandCardsPannel(Card card)
        {
            if (CardUiInstance == null)
                return;

            Debug.Log("Updating hand cards UI.");

            Object.Destroy(CardUiInstance.ExtractHandWidget(card).gameObject);

            CardUiInstance.RefreshAll();
            CardUiInstance.AdjustCardsPosition(true);
            CardUiInstance.RefreshAllCardsEdge();
        }
    }
}
