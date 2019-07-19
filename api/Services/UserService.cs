using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineChat.HttpResults;
using OnlineChat.HttpResults.Exceptions;
using OnlineChat.Models;
using OnlineChat.Repository;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Services
{
    public class UserService: IUserService
    {
        private readonly RepositoryContext _db;
        
        public UserService(RepositoryContext db)
        {
            _db = db;
        }
        public async Task<List<User>> All()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> Get(long id)
        {
            return await _db.Users.FindAsync(id);
        }

        public User GetByName(string name)
        {
            return _db.Users.FirstOrDefault( x => x.Name == name);
        }
        
        public async Task<ActionResult> Create(User user)
        {
            await _db.Users.AddAsync(user);
            return new HttpOk().ToJson();
        }
        
        public async Task<ActionResult> Delete(long id)
        {
            var user = await _db.Users.FindAsync(id);
            
            if (user == null) return new NotFoundHttpException(id).ToJson();
            
            _db.Users.Remove(user);
            return new HttpOk().ToJson();
        }

        public async Task<Chat> Chat(int id)
        {
            var user = await _db.Users.FindAsync(id);
            return user.Chat;
        }

        public async Task<ICollection<Message>> Messages(int id)
        {
            var user = await _db.Users.FindAsync(id);
            return user.Messages.ToList();
        }
    }
}