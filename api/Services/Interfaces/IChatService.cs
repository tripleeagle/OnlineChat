using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<Chat>> All();
        Task<Chat> Get(string name);
        Task<ActionResult> Create(Chat chat);
        Task<ActionResult> Delete(long id);
        
        Task<List<User>> Users(string name);
        Task<List<Message>> Messages(string name);
    }
}