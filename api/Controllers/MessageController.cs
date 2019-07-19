using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController: ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        [HttpGet]
        public async Task<ICollection<Message>> All()
        {
            return await _messageService.All();
        }

        [HttpGet("{id}")]
        public async Task<Message> Get(long id)
        {
            return await _messageService.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Message message)
        {
            return await _messageService.Create(message);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _messageService.Delete(id);
        }

        [HttpGet("{id}/chat")]
        public async Task<Chat> Chat(long id)
        {
            return await _messageService.Chat(id);
        }

        [HttpGet("{id}/user")]
        public async Task<User> User(long id)
        {
            return await _messageService.User(id);
        }
    }
}