using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Kafka.Producer.Api.Services;

namespace WebApi.Kafka.Producer.Api.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ProducerService _producerService;

        public MessagesController(ProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string message) 
        {
            var messageSended = await _producerService.SendMessage(message);

            return Ok(messageSended);
        }
    }
}
