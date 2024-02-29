using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Notifyer.Events;
using Notifyer.State;
using static Notifyer.Controllers.CommandsController;

namespace Notifyer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly ILogger<QueriesController> _logger;
        private readonly StatisticsState statisticsState;

        public QueriesController(ILogger<QueriesController> logger, StatisticsState statisticsState)
        {
            _logger = logger;
            this.statisticsState = statisticsState;
        }

        [HttpGet("[action]")]
        public Dictionary<string, int> Statistics()
        {
            return statisticsState.Statistics;
        }
    }
}
