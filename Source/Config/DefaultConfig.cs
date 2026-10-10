
using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using RNGCardPack.Source.Cards.Template;
using RNGCardPack.Source.Packs;
using System.Collections.Generic;
using UnityEngine;
using static LBoLEntitySideloader.Entities.EnemyGroupTemplate;

namespace RNGCardPack.Source.Config
{
    public sealed class RNGCardPackDefaultConfig
    {
        private static readonly string OwnerName = BepinexPlugin.modUniqueID;
        public static string DefaultID(EntityDefinition entity)
        {
            string IDdef = entity.GetType().Name;
            //Remove the Def at the end of the entity (class name) to get the ID. 
            //string ID = IDdef.Replace(@"Def", "");
            string ID = IDdef.Remove(IDdef.Length - 3);
            return ID;
        }

        // How to make an Hybrid color card:
        // config.Cost = new ManaGroup() { ..., Hybrid = amount, HybridColor = (int) color_code };

        // color_code: 
        // 0: Azorius (White/Blue)
        // 1: Orzhov (White/Black)
        // 2: Boros (White/Red)
        // 3: Selesnya (White/Green)
        // 4: Dimir (Blue/Black)
        // 5: Izzet (Blue/Red)
        // 6: Simic (Blue/Green)
        // 7: Rakdos (Black/Red)
        // 8: Golgari (Black/Green)
        // 9: Gruul (Red/Green)

        // Notation:
        // x: MtG Guild (Color 1/Color 2)
        // x being the number that has to be selected for the HybridColor field.

        public static CardConfig DefaultCardConfig()
        {
            return new CardConfig(
               Index: 0,
               Id: "",
               Order: 10,
               AutoPerform: true,
               Perform: new string[0][],
               GunName: "",
               GunNameBurst: "",
               DebugLevel: 0,
               Revealable: false,

               IsPooled: true,
               FindInBattle: true,

               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Common,
               Type: CardType.Unknown,
               TargetType: null,
               Colors: new List<ManaColor>() { },
               IsXCost: false,
               Cost: new ManaGroup() { },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: null,
               UpgradedValue1: null,
               Value2: null,
               UpgradedValue2: null,
               Mana: null,
               UpgradedMana: null,
               Kicker: null,
               UpgradedKicker: null,
               Scry: null,
               UpgradedScry: null,

               ToolPlayableTimes: null,

               Loyalty: null,
               UpgradedLoyalty: null,
               PassiveCost: null,
               UpgradedPassiveCost: null,
               ActiveCost: null,
               UpgradedActiveCost: null,
               ActiveCost2: null,
               UpgradedActiveCost2: null,
               UltimateCost: null,
               UpgradedUltimateCost: null,

               Keywords: Keyword.None,
               UpgradedKeywords: Keyword.None,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },

               Owner: null,
               Pack: nameof(RNGCardPackDef)[..^3],
               ImageId: "",
               UpgradeImageId: "",

               Unfinished: false,
               Illustrator: null,
               SubIllustrator: new List<string>() { }
            );
        }

        public static ExhibitConfig DefaultExhibitConfig()
        {
            return new ExhibitConfig(
                Index: 0,
                Id: "",
                Order: 10,
                IsDebug: false,
                IsPooled: false,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.Nowhere,
                Owner: OwnerName,
                LosableType: ExhibitLosableType.DebutLosable,
                Rarity: Rarity.Shining,
                Value1: null,
                Value2: null,
                Value3: null,
                Mana: new ManaGroup() { },
                BaseManaRequirement: null,
                BaseManaColor: ManaColor.White,
                BaseManaAmount: 1,
                HasCounter: false,
                InitialCounter: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { }
            );
        }

        public static StatusEffectConfig DefaultStatusEffectConfig()
        {
            return new StatusEffectConfig(
                Id: "",
                ImageId: null,
                Index: 0,
                Order: 10,
                Type: StatusEffectType.Positive,
                IsVerbose: false,
                IsStackable: true,
                StackActionTriggerLevel: null,
                HasLevel: true,
                LevelStackType: StackType.Add,
                HasDuration: false,
                DurationStackType: StackType.Add,
                DurationDecreaseTiming: DurationDecreaseTiming.Custom,
                HasCount: false,
                CountStackType: StackType.Keep,
                LimitStackType: StackType.Keep,
                ShowPlusByLimit: false,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                VFX: "Default",
                VFXloop: "Default",
                SFX: "Default"
            );
        }

        public static UltimateSkillConfig DefaultUltConfig()
        {
            return new UltimateSkillConfig(
                Id: "",
                Order: 10,
                PowerCost: 100,
                PowerPerLevel: 100,
                MaxPowerLevel: 2,
                RepeatableType: UsRepeatableType.OncePerTurn,
                Damage: 1,
                Value1: 0,
                Value2: 0,
                Keywords: Keyword.Accuracy,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { }
            );
        }

        public static JadeBoxConfig DefaultJadeBoxConfig()
        {
            return new JadeBoxConfig(
                Index: 0,
                Id: "",
                Order: 10,
                Group: new List<string>() { },
                Value1: null,
                Value2: null,
                Value3: null,
                Mana: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { }
            );
        }

        public static EnemyUnitConfig DefaultEnemyUnitConfig()
        {
            return new EnemyUnitConfig(
                Id: "",
                RealName: false,
                OnlyLore: false,
                BaseManaColor: new List<LBoL.Base.ManaColor>() { ManaColor.Colorless },
                Order: 10,
                ModleName: "",
                NarrativeColor: "#ffff",
                Type: EnemyType.Boss,
                IsPreludeOpponent: false,
                HpLength: null,
                MaxHpAdd: null,
                MaxHp: 250,
                Damage1: 10,
                Damage2: 10,
                Damage3: 10,
                Damage4: 10,
                Power: 1,
                Defend: 15,
                Count1: 1,
                Count2: 2,
                MaxHpHard: 250,
                Damage1Hard: 10,
                Damage2Hard: 10,
                Damage3Hard: 10,
                Damage4Hard: 10,
                PowerHard: 1,
                DefendHard: 15,
                Count1Hard: 1,
                Count2Hard: 2,
                MaxHpLunatic: 250,
                Damage1Lunatic: 10,
                Damage2Lunatic: 10,
                Damage3Lunatic: 10,
                Damage4Lunatic: 10,
                PowerLunatic: 1,
                DefendLunatic: 15,
                Count1Lunatic: 1,
                Count2Lunatic: 2,
                PowerLoot: new MinMax(100, 100),
                BluePointLoot: new MinMax(100, 100),
                Gun1: new List<string> { GunNameID.GetGunFromId(800) },
                Gun2: new List<string> { GunNameID.GetGunFromId(800) },
                Gun3: new List<string> { GunNameID.GetGunFromId(800) },
                Gun4: new List<string> { GunNameID.GetGunFromId(800) }
            );
        }

        public static EnemyGroupConfig DefaultEnemyGroupConfig()
        {
            return new EnemyGroupConfig(
                Id: "",
                Hidden: false,
                Environment: null,
                IsSub: false,
                Subs: new List<string>() { },
                Name: "",
                FormationName: VanillaFormations.Single,
                Enemies: new List<string>() { },
                EnemyType: EnemyType.Boss,
                DebutTime: 1f,
                RollBossExhibit: true,
                PlayerRoot: new Vector2(-4f, 0.5f),
                PreBattleDialogName: "",
                PostBattleDialogName: ""
            );
        }

        public static PackConfig DefaultPackConfig()
        {
            return new PackConfig(
                Id: "",

                CardList: new List<string>()
                {
                }
            );
        }

        public static GunConfig DefaultGunConfig()
        {
            return new GunConfig(
                Id: 120000,
                Name: "",
                Spell: "",
                Sequence: "",
                Animation: "shoot1",
                ForceHitTime: null,
                ForceHitAnimation: false,
                ForceHitAnimationSpeed: 0f,
                ForceShowEndStartTime: null,
                Shooter: "Direct",
                ShakePower: 1f
                );
        }

        public static PieceConfig DefaultPieceConfig()
        {
            return new PieceConfig(
                Id: 0,
                Type: false,
                Projectile: "DanmaFish",
                ShootType: 0,
                ParentPiece: 0,
                AddParentAngle: false,
                LastWave: true,
                LaserLastWave: 0,
                FollowPiece: 0,
                ShootEnd: 0,
                HitAmount: 1,
                HitInterval: 6,
                ZeroHitNotDie: false,
                Scale: new float[][] { },
                Color: new int[][] { new int[] { 0 } },
                RootType: 0,
                X: new float[][] { },
                Y: new float[][] { },
                Radius: new float[][] { },
                RadiusA: new float[][] { },
                Aim: 0,
                StartTime: 0,
                Group: 1,
                GInterval: 6,
                Way: new int[][] { new int[] { 1 } },
                GAngle: new float[][] { new float[] { 0 } },
                Range: new float[][] { new float[] { 0 } },
                Life: new int[][] { },
                StartSpeed: new float[][] { new float[] { 0 } },
                StartAcc: new float[][] { },
                StartAccAngle: new float[][] { },
                EvStart: new int[][][] { },
                EvDuration: new int[][][] { },
                EvNumber: new float[][][] { },
                EvType: new int[][] { },
                VanishV3: new Vector3(0.08f, 0.08f, 0.08f),
                LaunchSfx: "",
                HitBodySfx: "",
                HitAnimationSpeed: 1f
            );
        }
    }
}