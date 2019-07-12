using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Services
{
    public class UserService: IUserService
    {
        public Task<ActionResult<List<Message>>> All()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Message>> Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Create(User user)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<ActionResult> Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Chat>> Chat(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<List<User>>> Users(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}