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
        public async Task<ActionResult<List<User>>> All()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<ActionResult<User>> Get(long id)
        {
            return await _db.Users.FindAsync(id);
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

        public async Task<ActionResult<Chat>> Chat(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if ( user == null ) return new NotFoundHttpException(id).ToJson();
            return user.Chat;
        }

        public async Task<ActionResult<ICollection<Message>>> Messages(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if ( user == null ) return new NotFoundHttpException(id).ToJson();
            return user.Messages.ToList();
        }
    }
}