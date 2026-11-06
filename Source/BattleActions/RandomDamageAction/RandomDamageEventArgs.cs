using LBoL.Base;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using RNGCardPack.Source.BattleActions.RandomRangeAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.BattleActions.RandomDamageAction
{
    public class RandomDamageEventArgs : GameEventArgs
    {
        public Unit Source { get; set; }
        public Unit[] Targets { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int MedianDamage { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsAcurracy { get; set; }

        public string GunName { get; set; }

        public GunType GunType { get; set; }

        public bool AllowLuck { get; set; }
        
        public bool AllowOrderchaos { get; set; }

        public bool AllowNegativeValues { get; set; }

        public int Result { get; set; }

        public Dictionary<string, RollRandomRangeAction> RollActions { get; set; }

        protected override string GetBaseDebugString()
        {
            if (Targets.Length > 1)
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append((($"{GameEventArgs.DebugString(Source)} --- ") + ((IsAcurracy) ? $"{{{MinDamage}~{MaxDamage}, Accuracy}}" : $"{{{MinDamage}~{MaxDamage}}}")) + "--> ");

                for (int i = 0; i < Targets.Length; i++)
                {
                    if (i == 0)
                        stringBuilder.Append("[");

                    stringBuilder.Append(GameEventArgs.DebugString(Targets[i]));

                    if (i != Targets.Length - 1)
                        stringBuilder.Append(", ");
                }

                stringBuilder.Append("]");

                return stringBuilder.ToString();
            }

            else
                return (($"{GameEventArgs.DebugString(Source)} --- ") + ((IsAcurracy) ? $"{{{MinDamage}~{MaxDamage}, Accuracy}}" : $"{{{MinDamage}~{MaxDamage}}}") + ($"--> {GameEventArgs.DebugString(Targets[0])}"));
        }
    }
}
