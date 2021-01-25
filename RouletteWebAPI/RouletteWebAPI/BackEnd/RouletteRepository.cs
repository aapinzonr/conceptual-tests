using RouletteWebAPI.Domain;
using System;
using System.Collections.Generic;

namespace RouletteWebAPI.BackEnd
{
    public class RouletteRepository
    {
        private const string ROUTE_CODES_SEPARATOR = ",";
        private const string ACTIVE_ROULETTES_KEY = "ActiveRoulettes";

        [ThreadStatic]
        private RedisRepository _Repository;
        
        public RedisRepository Repository
        {
            get
            {
                if (this._Repository == null)
                {
                    this._Repository = new RedisRepository(connectionString: this.ConnectionString);
                }
                return this._Repository;
            }
        }

        public string ConnectionString { get; set; }

        public RouletteRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private void AppendRoulettes(int rouletteId)
        {
            var activeRouletesInfo = this.Repository.Get<string>(key: ACTIVE_ROULETTES_KEY);
            if (string.IsNullOrEmpty(activeRouletesInfo))
            {
                activeRouletesInfo = string.Concat(activeRouletesInfo, rouletteId);
            }
            else
            {
                activeRouletesInfo = string.Concat(activeRouletesInfo, ROUTE_CODES_SEPARATOR, rouletteId);
            }
            this.Repository.Save<string>(key: ACTIVE_ROULETTES_KEY, value: activeRouletesInfo);
        }

        public IEnumerable<Roulette> GetAll()
        {
            IList<Roulette> activeRoulettes = null;
            var activeRouletesInfo = this.Repository.Get<string>(key: ACTIVE_ROULETTES_KEY);
            if (!string.IsNullOrEmpty(activeRouletesInfo))
            {
                var activeRouletesCodes = activeRouletesInfo.Split(separator: ROUTE_CODES_SEPARATOR, options: StringSplitOptions.RemoveEmptyEntries);
                if (activeRouletesCodes.Length > 0)
                {
                    activeRoulettes = new List<Roulette>();
                    foreach (var rouletteCode in activeRouletesCodes)
                    {
                        var rouletteId = Convert.ToInt32(rouletteCode);
                        var rouletteInfo = this.GetById(rouletteId: rouletteId);
                        if (rouletteInfo != null)
                        {
                            activeRoulettes.Add(rouletteInfo);
                        }
                    }
                }
            }
            return activeRoulettes;
        }

        public int Create(Roulette roulette)
        {
            int rouletteId = 0;
            if (roulette != null)
            {
                rouletteId = Math.Abs((int)DateTime.Today.Ticks - (int)DateTime.UtcNow.Ticks);
                roulette.RouletteId = rouletteId;
                var rouletteKey = rouletteId.ToString();
                this.Repository.Save<Roulette>(key: rouletteKey, value: roulette);
                this.AppendRoulettes(rouletteId: rouletteId);
            }
            return rouletteId;
        }

        public Roulette GetById(int rouletteId)
        {
            Roulette roulette = this.Repository.Get<Roulette>(key: rouletteId.ToString());
            return roulette;
        }

        public bool Open(Roulette roulette)
        {
            bool operationSucess = false;
            if (roulette != null && roulette.RouletteId > 0)
            {
                roulette.IsOpen = true;
                var rouletteKey = roulette.RouletteId.ToString();
                this.Repository.Save<Roulette>(key: rouletteKey, value: roulette);
                operationSucess = true;
            }
            return operationSucess;
        }

        public void SaveBets(Roulette roulette)
        {
            if (roulette != null && roulette.HasBets)
            {
                var rouletteKey = roulette.RouletteId.ToString();
                this.Repository.Save<Roulette>(key: rouletteKey, value: roulette);
            }
        }
    }
}
