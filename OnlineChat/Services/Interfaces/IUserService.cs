using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<List<Message>>> All();
        Task<ActionResult<Message>> Get(long id);
        Task<ActionResult> Create(User user);
        Task<ActionResult> Delete(long id);
        
        Task<ActionResult<Chat>> Chat(int id);
        Task<ActionResult<List<User>>> Users(int id);
    }
}