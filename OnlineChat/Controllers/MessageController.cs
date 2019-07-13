using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    public class MessageController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        public async Task<ActionResult<List<Message>>> All()
        {
            return await _messageService.All();
        }

        public async Task<ActionResult<Message>> Get(long id)
        {
            return await _messageService.Get(id);
        }

        public async Task<ActionResult> Create(Message message)
        {
            return await _messageService.Create(message);
        }

        public async Task<ActionResult> Delete(long id)
        {
            return await _messageService.Delete(id);
        }

        public async Task<ActionResult<Chat>> Chat(long id)
        {
            return await _messageService.Chat(id);
        }

        public async Task<ActionResult<User>> User(long id)
        {
            return await _messageService.User(id);
        }
    }
}