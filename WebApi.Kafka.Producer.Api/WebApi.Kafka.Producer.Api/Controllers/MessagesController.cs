using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Kafka.Producer.Api.Models;
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
        public async Task<IActionResult> Post(PessoaModel model) 
        {
            var messageSended = await _producerService.SendMessage(model);

            return Ok(messageSended);
        }
    }
}
