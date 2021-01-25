using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteWebAPI.Domain
{
    [Serializable]
    public class Roulette
    {
        #region Constants
        private const float NUMERIC_BET_WIN_FACTOR = 5.0f;
        private const float COLOR_BET_WIN_FACTOR = 1.8f;
        public const int MIN_NUMBER = 0;
        public const int MAX_NUMBER = 36;
        public const string RED_COLOR_VALUE = "Red";
        public const string BLACK_COLOR_VALUE = "Black";
        #endregion Constants

        #region Properties
        public int RouletteId { get; set; }

        public string UserId { get; set; }

        public bool IsOpen { get; set; }

        public int WinningNumber { get; set; } = -1;

        public string WinningColor
        {
            get
            {
                string winningColor = this.WinningNumber % 2 == 0 ? RED_COLOR_VALUE : BLACK_COLOR_VALUE;
                return winningColor;
            }
        }

        private IList<BetInfo> _Bets;

        public IList<BetInfo> Bets
        {
            get
            {
                if (this._Bets == null)
                {
                    this._Bets = new List<BetInfo>();
                }
                return this._Bets;
            }
            private set
            {
            }
        }

        public bool HasBets
        {
            get
            {
                return this._Bets != null && this._Bets.Any();
            }
        }
        #endregion Properties

        #region Methods
        public void MakeBet(BetInfo betInfo)
        {
            betInfo.RouletteId = this.RouletteId;
            this.Bets.Add(betInfo);
        }

        public void Close()
        {
            this.WinningNumber = this.Run();
            this.CheckBets();
            this.IsOpen = false;
        }

        private void CheckBets()
        {
            if (this.HasBets)
            {
                foreach (BetInfo bet in this.Bets)
                {
                    this.CheckBet(bet);
                }
            }
        }

        private void CheckBet(BetInfo bet)
        {
            if (bet != null && this.WinningNumber >= 0)
            {
                bool isNumericBet = int.TryParse(bet.ExpectedValue, out int bettedNumber);
                bool isWinnerBet = isNumericBet ? bettedNumber.Equals(this.WinningNumber) : bet.ExpectedValue.Equals(this.WinningColor);
                float winFactor = 0;
                if (isWinnerBet)
                {
                    winFactor = isNumericBet ? NUMERIC_BET_WIN_FACTOR : COLOR_BET_WIN_FACTOR;
                }
                bet.WinnedValue = bet.StakeAmount * winFactor;
            }
        }

        private int Run()
        {
            int winningNumber = -1;
            bool isReadyToRun = this.IsOpen && this.HasBets;
            if (isReadyToRun)
            {
                Random random = new Random();
                winningNumber = random.Next(MIN_NUMBER, MAX_NUMBER);
            }
            return winningNumber;
        }
        #endregion Methods
    }
}
