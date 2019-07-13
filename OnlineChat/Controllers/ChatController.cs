using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController: ControllerBase
    {
        private readonly IChatService _chatService;
        
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> All()
        {
            return await _chatService.All();
        }
        
        [HttpGet("{name}")]
        public async Task<ActionResult<Chat>> Get(string name)
        {
            return await _chatService.Get(name);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Chat chat)
        {
            return await _chatService.Create(chat);
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _chatService.Delete(id);
        }
        
        [HttpGet("{id}/users")]
        public async Task<ActionResult<List<User>>> Users(string name)
        {
            return await _chatService.Users(name);
        }
        
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<List<Message>>> Messages(string name)
        {
            return await _chatService.Messages(name);
        }
    }
}