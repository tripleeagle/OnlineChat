using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<ActionResult<List<Chat>>> All();
        Task<ActionResult<Chat>> Get(long id);
        Task<ActionResult> Create(Chat chat);
        Task<ActionResult> Delete(long id);
        
        Task<ActionResult<List<User>>> Users(int id);
        Task<ActionResult<List<Message>>> Messages(int id);
    }
}