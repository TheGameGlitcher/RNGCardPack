using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LBoL.Base;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.EntityLib.PlayerUnits;
using LBoL.Presentation;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.PersistentValues;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Cards.Template;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



namespace RNGCardPack
{
    [BepInPlugin(PInfo.GUID, PInfo.Name, PInfo.version)]
    [BepInDependency(LBoLEntitySideloader.PluginInfo.GUID, BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency(AddWatermark.API.GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LBoL.exe")]
    public class BepinexPlugin : BaseUnityPlugin
    {
        //The Unique mod ID of the mod.
        //If defined, this is also the ID used by the Act 1 boss.
        //WARNING: It is mandatory to rename it to avoid issues.
        public static string modUniqueID = "RNGCardPack";

        public static List<ManaColor> offColors = new List<ManaColor>() { };

        private static readonly Harmony harmony = PInfo.harmony;

        internal static BepInEx.Logging.ManualLogSource log;

        internal static TemplateSequenceTable sequenceTable = new TemplateSequenceTable();

        internal static IResourceSource embeddedSource = new EmbeddedSource(Assembly.GetExecutingAssembly());

        internal static DirectorySource directorySource = new DirectorySource(PInfo.GUID, "");

        internal static RNGCardPackSaveData saveData = new RNGCardPackSaveData();


        private void Awake()
        {
            log = Logger;

            // very important. Without this the entry point MonoBehaviour gets destroyed
            DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            harmony.PatchAll();

            saveData.RegisterSelf(PInfo.GUID);
            CardIndexGenerator.PromiseClearIndexSet();
            EntityManager.RegisterSelf();

            //Watermark is now unused as of 1.8.0.
            //The code is temporarily kept if the devs change their minds.
            /*if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AddWatermark.API.GUID))
                WatermarkWrapper.ActivateWatermark();*/
        }

        public class RNGCardPackSaveData : CustomGameRunSaveData
        {
            public List<int> SavedGenericCostCards = new List<int>();

            public override void Save(GameRunController gameRun)
            {
                log.LogDebug("RNGCardPack bepinex starting saving");

                SavedGenericCostCards = RNGCardPackCard.GenericCostCards;

                log.LogDebug("RNGCardPack bepinex finished saving");
            }

            public override void Restore(GameRunController gameRun)
            {
                log.LogDebug("RNGCardPack bepinex starting restoring");

                RestoreVariables(gameRun);
                RestoreGenericCardCosts(gameRun);

                log.LogDebug("RNGCardPack bepinex finished restoring");
            }

            private void RestoreVariables(GameRunController gameRun)
            {
                RNGCardPackCard.GenericCostCards = SavedGenericCostCards;
            }

            private void RestoreGenericCardCosts(GameRunController gameRun)
            {
                Card deckCardByInstanceId;
                ManaGroup genericCost;
                ManaGroup? upgradedGenericCost;

                foreach (int instanceId in SavedGenericCostCards)
                {
                    deckCardByInstanceId = gameRun.GetDeckCardByInstanceId(instanceId);
                    genericCost = new ManaGroup() { Any = deckCardByInstanceId.Config.Cost.Amount };

                    if (deckCardByInstanceId.Config.UpgradedCost != null)
                        upgradedGenericCost = new ManaGroup() { Any = (int)deckCardByInstanceId.Config.UpgradedCost?.Amount };

                    else
                        upgradedGenericCost = null;



                    deckCardByInstanceId.SetBaseCost(genericCost);
                    deckCardByInstanceId.Config.Cost = genericCost;
                    deckCardByInstanceId.Config.UpgradedCost = upgradedGenericCost;
                }
            }
        }


        private void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchSelf();
        }
    }
}
