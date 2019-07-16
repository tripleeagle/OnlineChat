using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ActionResult<List<Message>>> All();
        Task<ActionResult<Message>> Get(long id);
        Task<ActionResult> Create(Message message);
        Task<ActionResult> Delete(long id);
        
        Task<ActionResult<Chat>> Chat(long id);
        Task<ActionResult<User>> User(long id);
    }
}