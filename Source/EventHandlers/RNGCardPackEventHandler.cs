using LBoL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RNGCardPack.Source.EventHandlers
{
    public sealed class RNGCardPackEventHandler
    {
        private static RNGCardPackEventHandler instance = null;

        public event EventHandler PercentRollFail;
        public event EventHandler PercentRollSuccess;

        public static RNGCardPackEventHandler Instance
        {
            get
            {
                instance ??= new RNGCardPackEventHandler();

                return instance;
            }
        }

        public void NotifyPercentRollFail()
        {
            PercentRollFail?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyPercentRollSuccess()
        {
            PercentRollSuccess?.Invoke(this, EventArgs.Empty);
        }
    }
}
