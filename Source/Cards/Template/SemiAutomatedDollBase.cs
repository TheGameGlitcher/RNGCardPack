using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Neutral.Blue;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Cards.Neutral.TwoColor;
using LBoL.EntityLib.Cards.Neutral.White;
using LBoL.EntityLib.Cards.Others;
using LBoL.EntityLib.Cards.Tool;
using RNGCardPack.Source.Cards.Colors.B;
using RNGCardPack.Source.Cards.Colors.C;
using RNGCardPack.Source.Cards.Colors.G;
using RNGCardPack.Source.Cards.Colors.P;
using RNGCardPack.Source.Cards.Colors.R;
using RNGCardPack.Source.Cards.Colors.U;
using RNGCardPack.Source.Cards.Colors.W;
using RNGCardPack.Source.StatusEffects.Effects;
using System;
using System.Collections.Generic;

namespace RNGCardPack.Source.Cards.Template
{
    public class SemiAutomatedDollBase : RNGCardPackCard
    {
        public static List<Type> GenericColors { get; } = new List<Type>() { typeof(SemiAutomatedDollW), typeof(SemiAutomatedDollU), typeof(SemiAutomatedDollB),
                                                                             typeof(SemiAutomatedDollR), typeof(SemiAutomatedDollG), typeof(SemiAutomatedDollC) };

        public static Type PhilosopherColor { get; } = typeof(SemiAutomatedDollP);

        public static List<Type> TokenCards { get; } = new List<Type>() { typeof(WManaCard), typeof(UManaCard), typeof(BManaCard), typeof(RManaCard), typeof(GManaCard), typeof(CManaCard),
                                                                          typeof(PManaCard), typeof(Fengmozhen), typeof(Shenyuzha), typeof(YinyangCard), typeof(ShuihuoCard), typeof(FengleiCard),
                                                                          typeof(ShanyuCard), typeof(Xiaozhuo), typeof(Astrology), typeof(Potion), typeof(StarlightTaifeng), typeof(RedMogu),
                                                                          typeof(PurpleMogu), typeof(BlueMogu), typeof(Knife), typeof(GroupOfKnife), typeof(SummerFlower), typeof(IceWing),
                                                                          typeof(IceBolt), typeof(YuetuYuyi), typeof(FakeMoon), typeof(MeilingLianhua), typeof(MeteonicDebris) };

        public static List<Type> Statuses { get; } = new List<Type>() { typeof(Firepower), typeof(Spirit), typeof(LuckSe), typeof(OrderSe) };

        public static List<Type> ToolGadgetCardsGeneric { get; } = new List<Type>() { typeof(ShopAttack), typeof(ShopDefense), typeof(ToolDraw), typeof(ToolHeal), typeof(ToolWeak),
                                                                                      typeof(ToolVulnerable), typeof(ToolAttack), typeof(ToolBlock), typeof(ToolAmulet), typeof(ToolMana),
                                                                                      typeof(ToolMicai), typeof(ToolUpgrade), typeof(ToolFirstAid), typeof(ToolExile) };

        public static List<Type> ToolGadgetCardsRare { get; } = new List<Type>() { typeof(ShopSearch), typeof(ToolInvincible), typeof(ToolMaxHp), typeof(ToolDroneBlock), typeof(ToolRandomTool) };
    }
}
