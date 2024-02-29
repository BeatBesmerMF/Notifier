using System.Security.Cryptography;
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
        private readonly EventStore eventStore;

        public CommandsController(ILogger<CommandsController> logger, EventStore eventStore)
        {
            _logger = logger;
            this.eventStore = eventStore;
        }

        [HttpPost("[action]")]
        public IActionResult SubscribeToMachine(SubscriptionDetail subscriptionDetail)
        {
            if (String.IsNullOrEmpty(subscriptionDetail.UserId) ||
                String.IsNullOrEmpty(subscriptionDetail.MachineId))
            {
                return BadRequest("IDs cannot be empty!");
            }

            var subscriptionId = Guid.NewGuid().ToString();

            eventStore.StoreEvents([new Event<UserSubscribedToMachine>(
                Subject: $"/subscriptions/{subscriptionId}",
                Data: new (
                    UserId: subscriptionDetail.UserId,
                    MachineId: subscriptionDetail.MachineId
                )
            )]);

            return Ok();
        }
    }
}
