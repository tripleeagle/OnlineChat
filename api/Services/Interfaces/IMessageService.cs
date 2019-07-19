using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> All();
        Task<Message> Get(long id);
        Task<ActionResult> Create(Message message);
        Task<ActionResult> Delete(long id);
        
        Task<Chat> Chat(long id);
        Task<User> User(long id);
    }
}