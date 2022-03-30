using Microsoft.AspNetCore.Mvc;
using Oz.Bet.Broker.Services;

namespace Oz.Bet.Broker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetSlipController : ControllerBase
    {
        private readonly ILogger<BetSlipController> logger;
        private readonly BetslipService betSlipService;

        public BetSlipController(ILogger<BetSlipController> logger, BetslipService betSlipService)
        {
            this.logger = logger;
            this.betSlipService = betSlipService;
        }


        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If Arguments have problem </response>
        [HttpPost(Name = "PlaceBetSlip")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] BetSlipRequest betSlip)
        {
            var user = "asdad";//Authorization User.Identity.Name
            var bet = new BetSlipContext("web", user, betSlip.GameId, betSlip.Market, betSlip.Outcome, betSlip.Amount, betSlip.Odd);

            var res = await betSlipService.CreateAsync(bet);
            if (res.Success)
                return Accepted(res.Result);
            else
                return BadRequest(res.Result);
        }
    }
}