using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.HttpResults;
using OnlineChat.HttpResults.Exceptions;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<User>>> All()
        {
            return await _userService.All();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id)
        {
            return await _userService.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            return await _userService.Create(user);
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _userService.Delete(id);
        }

        [HttpGet("{id}/chat")]
        public async Task<ActionResult<Chat>> Chat(int id)
        {
            return await _userService.Chat(id);
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult<ICollection<Message>>> Messages(int id)
        {
            return await _userService.Messages(id);
        }
    }
}