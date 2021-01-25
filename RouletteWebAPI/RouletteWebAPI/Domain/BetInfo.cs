using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteWebAPI.Domain
{
    [Serializable]
    public class BetInfo
    {
        public int StakeAmount { get; set; }

        public string ExpectedValue { get; set; }

        public int RouletteId { get; set; }

        public float WinnedValue { get; set; }

        public bool IsWinner
        {
            get
            {
                return this.WinnedValue > 0;
            }
        }
    }
}
