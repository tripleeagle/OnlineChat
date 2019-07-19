using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;

namespace OnlineChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> All();
        Task<User> Get(long id);
        User GetByName(string name);
        Task<ActionResult> Create(User user);
        Task<ActionResult> Delete(long id);
        
        Task<Chat> Chat(int id);
        Task<ICollection<Message>> Messages(int id);
    }
}