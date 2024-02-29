using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace Notifyer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {

        private readonly ILogger<CommandsController> _logger;

        public CommandsController(ILogger<CommandsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public void SubscribeToMachine()
        {
            return;
        }
    }
}
