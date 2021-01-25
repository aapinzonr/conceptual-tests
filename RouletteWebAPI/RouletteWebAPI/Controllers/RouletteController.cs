using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using RouletteWebAPI.Domain;

namespace RouletteWebAPI.Controllers
{
    /// <summary>
    /// Group and exposes the virtual roulette API Endpoints.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        #region Constants
        /// <summary>
        /// Custom header key of user Id.
        /// </summary>
        private const string USER_ID_KEY = "userId";
        #endregion Constants

        #region Properties
        /// <summary>
        /// Service configurarion settings.
        /// </summary>
        private IConfiguration Configuration;
       
        /// <summary>
        /// User Id of a virtual roulette.
        /// </summary>
        private string UserId
        {
            get
            {
                string userId = string.Empty;
                if (Request.Headers.TryGetValue(key: USER_ID_KEY, out StringValues headerValues))
                {
                    userId = headerValues.FirstOrDefault();
                }
                return userId;
            }
        }

        /// <summary>
        /// Connection string to redis cache repository
        /// </summary>
        private string RedisConnectionString
        {
            get
            {
                string connectionString = this.Configuration.GetConnectionString(name: nameof(this.RedisConnectionString));
                return connectionString;
            }
        }

        private RouletteBll _RouletteFacade;

        /// <summary>
        /// Roulettes data management facade.
        /// </summary>
        private RouletteBll RouletteFacade
        {
            get
            {
                if (this._RouletteFacade == null)
                {
                    this._RouletteFacade = new RouletteBll(connectionString: this.RedisConnectionString);
                }
                return this._RouletteFacade;
            }
        }
        #endregion Properties

        /// <summary>
        /// Initialize a new instance of <see cref="RouletteController"/> class.
        /// </summary>
        /// <param name="configuration">Service configuration settings instance.</param>
        public RouletteController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #region Methods
        /// <summary>
        /// Endpoint to create a virtual roulette.
        /// </summary>
        /// <returns>Roulette Id of created roulette.</returns>
        /// <remarks>
        /// API Route: PUT [RouletteController]
        /// </remarks>
        [HttpPut]
        public int CreateRoulette()
        {
            int rouletteId = 0;
            if (!string.IsNullOrEmpty(this.UserId))
            {
                rouletteId = this.RouletteFacade.Create(userId: this.UserId);
                Response.StatusCode = (int)HttpStatusCode.Created;
            }
            return rouletteId;
        }

        /// <summary>
        /// Endpoint to open an existing roulette.
        /// </summary>
        /// <param name="rouletteId">Roulette Id.</param>
        /// <returns>true if operation has been sucessful, otherwise false.</returns>
        /// <remarks>
        /// API Route: POST [RouletteController]/{rouletteId}
        /// </remarks>
        [HttpPost("{rouletteId}")]
        public bool OpenRoulette(int rouletteId)
        {
            bool operationSucess = false;
            if (rouletteId > 0)
            {
                operationSucess = this.RouletteFacade.OpenRoulette(rouletteId: rouletteId);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return operationSucess;
        }

        /// <summary>
        /// Endpoint to receive and register a new bet.
        /// </summary>
        /// <param name="betInfo">Info to create a new bet on an opened roulette.</param>
        /// <remarks>
        /// API Route: POST [RouletteController]/bets/{rouletteId}
        /// </remarks>
        [HttpPost("bets/{rouletteId}")]
        public void MakeBet([FromBody] BetInfo betInfo)
        {
            var rouletteIdParameter = Request.RouteValues["rouletteId"] as string;
            bool isValidRouletteId = int.TryParse(rouletteIdParameter, out int rouletteId);
            if (isValidRouletteId && !string.IsNullOrEmpty(this.UserId))
            {
                this.RouletteFacade.MakeBet(betInfo: betInfo, rouletteId: rouletteId, userId: this.UserId);
                Response.StatusCode = (int)HttpStatusCode.Created;
            }
        }

        /// <summary>
        /// Endpoint to close bets of a given roulette.
        /// </summary>
        /// <param name="rouletteId">Roulette Id.</param>
        /// <returns>Results set of a roulette maked bets.</returns>
        /// <remarks>
        /// API Route: GET [RouletteController]/close/{rouletteId}
        /// </remarks>
        [HttpGet("close/{rouletteId}")]
        public IEnumerable<BetInfo> CloseBets(int rouletteId)
        {
            IEnumerable<BetInfo> closedBets = null;
            if (rouletteId > 0)
            {
                closedBets = this.RouletteFacade.CloseBets(rouletteId: rouletteId);
            }
            return closedBets;
        }

        /// <summary>
        /// Endpoint to list an existing roulettes.
        /// </summary>
        /// <returns>Set of created roulettes.</returns>
        /// <remarks>
        /// API Route: GET [RouletteController]
        /// </remarks>
        [HttpGet]
        public IEnumerable<Roulette> GetRoulettes()
        {
            IEnumerable<Roulette> roulettes = this.RouletteFacade.GetRoulettes();
            return roulettes;
        }
        #endregion Methods
    }
}
