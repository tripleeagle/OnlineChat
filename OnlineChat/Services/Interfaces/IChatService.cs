using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<ActionResult<List<Chat>>> All();
        Task<ActionResult<Chat>> Get(string name);
        Task<ActionResult> Create(Chat chat);
        Task<ActionResult> Delete(long id);
        
        Task<ActionResult<List<User>>> Users(string name);
        Task<ActionResult<List<Message>>> Messages(string name);
    }
}