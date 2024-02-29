using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Notifyer.Events;

namespace Notifyer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        public record SubscriptionDetail(
        string UserId,
        string MachineId
        );

        private readonly ILogger<CommandsController> _logger;

        public CommandsController(ILogger<CommandsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public IActionResult SubscribeToMachine(SubscriptionDetail subscriptionDetail)
        {
            if (String.IsNullOrEmpty(subscriptionDetail.UserId) ||
                String.IsNullOrEmpty(subscriptionDetail.MachineId))
            {
                return BadRequest("IDs cannot be empty!");
            }

            Console.WriteLine(new Event<UserSubscribedToMachine>(
                Subject: Request.GetDisplayUrl(),
                Data: new (
                    UserId: subscriptionDetail.UserId,
                    MachineId: subscriptionDetail.MachineId
                )
            ));

            return Ok();
        }
    }
}
