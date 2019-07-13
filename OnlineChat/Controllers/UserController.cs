using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.HttpResults;
using OnlineChat.HttpResults.Exceptions;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<ActionResult<List<User>>> All()
        {
            return await _userService.All();
        }

        public async Task<ActionResult<User>> Get(long id)
        {
            return await _userService.Get(id);
        }

        public async Task<ActionResult> Create(User user)
        {
            return await _userService.Create(user);
        }
        
        public async Task<ActionResult> Delete(long id)
        {
            return await _userService.Delete(id);
        }

        public async Task<ActionResult<Chat>> Chat(int id)
        {
            return await _userService.Chat(id);
        }

        public async Task<ActionResult<ICollection<Message>>> Messages(int id)
        {
            return await _userService.Messages(id);
        }
    }
}