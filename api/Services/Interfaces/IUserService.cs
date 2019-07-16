using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<List<User>>> All();
        Task<ActionResult<User>> Get(long id);
        Task<ActionResult> Create(User user);
        Task<ActionResult> Delete(long id);
        
        Task<ActionResult<Chat>> Chat(int id);
        Task<ActionResult<ICollection<Message>>> Messages(int id);
    }
}