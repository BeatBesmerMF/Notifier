using Microsoft.AspNetCore.Mvc;

namespace Notifyer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        public record SubscriptionDetail(
        String userId,
        String machineId
        );


        private readonly ILogger<CommandsController> _logger;

        public CommandsController(ILogger<CommandsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public void SubscribeToMachine(SubscriptionDetail subscriptionDetail)
        {
            Console.WriteLine(subscriptionDetail);
            return;
        }
    }
}
