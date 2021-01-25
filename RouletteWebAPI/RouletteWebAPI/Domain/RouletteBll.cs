using RouletteWebAPI.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteWebAPI.Domain
{
    /// <summary>
    /// Facade that expose actions to manipulate a virtual roulettes system.
    /// </summary>
    public class RouletteBll
    {
        #region Properties
        public string ConnectionString { get; set; }

        private RouletteRepository _RouletteRepository;

        public RouletteRepository RouletteRepository
        {
            get
            {
                if (this._RouletteRepository == null)
                {
                    this._RouletteRepository = new RouletteRepository(connectionString: this.ConnectionString);
                }
                return this._RouletteRepository;
            }
        }
        #endregion Properties

        public RouletteBll(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #region Methods
        public int Create(string userId)
        {
            int rouletteId = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                Roulette roulette = new Roulette
                {
                    UserId = userId,
                    IsOpen = false
                };
                rouletteId = this.RouletteRepository.Create(roulette: roulette);
            }
            return rouletteId;
        }

        public bool OpenRoulette(int rouletteId)
        {
            bool operationSucess = false;
            Roulette rouletteInfo = this.RouletteRepository.GetById(rouletteId: rouletteId);
            if (rouletteInfo != null)
            {
                operationSucess = this.RouletteRepository.Open(roulette: rouletteInfo);
            }
            return operationSucess;
        }

        public void MakeBet(BetInfo betInfo, int rouletteId, string userId)
        {
            Roulette rouletteInfo = this.RouletteRepository.GetById(rouletteId: rouletteId);
            if (rouletteInfo != null && rouletteInfo.IsOpen)
            {
                var rouletteOwner = rouletteInfo.UserId;
                bool isSameRouletteUser = rouletteOwner.Equals(userId);
                if (isSameRouletteUser)
                {
                    if (this.IsValidBet(betInfo))
                    {
                        rouletteInfo.MakeBet(betInfo);
                        this.RouletteRepository.SaveBets(roulette: rouletteInfo);
                    }
                }
            }
        }

        private bool IsValidBet(BetInfo betInfo)
        {
            bool isValid = false;
            const int MAX_BET_AMOUNT = 10000;
            if (betInfo != null)
            {
                bool isValidAmount = betInfo.StakeAmount > 0 && betInfo.StakeAmount <= MAX_BET_AMOUNT;
                bool isNumericBet = int.TryParse(betInfo.ExpectedValue, out int bettedNumber);
                bool isValidBetValue = isNumericBet ? bettedNumber >= Roulette.MIN_NUMBER && bettedNumber <= Roulette.MAX_NUMBER : betInfo.ExpectedValue.Equals(Roulette.RED_COLOR_VALUE) || betInfo.ExpectedValue.Equals(Roulette.BLACK_COLOR_VALUE);
                isValid = isValidAmount && isValidBetValue;
            }
            return isValid;
        }

        public IEnumerable<BetInfo> CloseBets(int rouletteId)
        {
            IEnumerable<BetInfo> closedBets = null;
            Roulette rouletteInfo = this.RouletteRepository.GetById(rouletteId: rouletteId);
            if (rouletteInfo != null)
            {
                if (rouletteInfo.IsOpen)
                {
                    rouletteInfo.Close();
                    this.RouletteRepository.SaveBets(roulette: rouletteInfo);
                }
                closedBets = rouletteInfo.Bets;
            }
            return closedBets;
        }

        public IEnumerable<Roulette> GetRoulettes()
        {
            IEnumerable<Roulette> roulettes = this.RouletteRepository.GetAll();
            return roulettes;
        }
        #endregion Methods
    }
}
