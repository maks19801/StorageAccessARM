using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessageProducer.Services;
using System.Threading;

namespace MessageProducer.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly QueueService queueService;

        public MessageController(QueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpGet]
        public string Get() => "Running";

        // POST api/message
        [HttpPost]
        public async Task Post([FromBody] dynamic value, CancellationToken cancellationToken)
        {
            await queueService.Queue1.SendMessageAsync(value.ToString(), cancellationToken);
        }
    }
}
