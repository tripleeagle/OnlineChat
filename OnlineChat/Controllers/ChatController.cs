using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    public class ChatController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        
        public async Task<ActionResult<List<Chat>>> All()
        {
            return await _chatService.All();
        }

        public async Task<ActionResult<Chat>> Get(long id)
        {
            return await _chatService.Get(id);
        }

        public async Task<ActionResult> Create(Chat chat)
        {
            return await _chatService.Create(chat);
        }

        public async Task<ActionResult> Delete(long id)
        {
            return await _chatService.Delete(id);
        }

        public async Task<ActionResult<List<User>>> Users(int id)
        {
            return await _chatService.Users(id);
        }

        public async Task<ActionResult<List<Message>>> Messages(int id)
        {
            return await _chatService.Messages(id);
        }
    }
}