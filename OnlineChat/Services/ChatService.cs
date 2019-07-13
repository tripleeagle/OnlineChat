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
    public class ChatService: IChatService
    {
        private readonly RepositoryContext _db;
        
        public ChatService(RepositoryContext db)
        {
            _db = db;
        }
        
        public async Task<ActionResult<List<Chat>>> All()
        {
            return await _db.Chats.ToListAsync();
        }

        public async Task<ActionResult<Chat>> Get(string name)
        {
            return await _db.Chats.FindAsync(name);
        }
        
        public async Task<ActionResult> Create(Chat chat)
        {
            await _db.Chats.AddAsync(chat);
            return new HttpOk().ToJson();
        }

        public async Task<ActionResult> Delete(long id)
        {
            var chat = await _db.Chats.FindAsync(id);
            
            if (chat == null) return new NotFoundHttpException(id).ToJson();
            
            _db.Chats.Remove(chat);
            return new HttpOk().ToJson();
        }

        public async Task<ActionResult<List<User>>> Users(string name)
        {
            var chat = await _db.Chats.FindAsync(name);
            if ( chat == null ) return new NotFoundHttpException(name).ToJson();
            return chat.Users.ToList();
        }

        public async Task<ActionResult<List<Message>>> Messages(string name)
        {
            var chat = await _db.Chats.FindAsync(name);
            if ( chat == null ) return new NotFoundHttpException(name).ToJson();
            return chat.Messages.ToList();
        }
    }
}