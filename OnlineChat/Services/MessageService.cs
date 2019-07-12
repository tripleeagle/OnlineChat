

using System.Collections.Generic;
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
    public class MessageService: IMessageService
    {
        private readonly RepositoryContext _db;
        
        public MessageService(RepositoryContext db)
        {
            _db = db;
        }
        public async Task<ActionResult<List<Message>>> All()
        {
            return await _db.Messages.ToListAsync();
        }

        public async Task<ActionResult<Message>> Get(long id)
        {
            return await _db.Messages.FindAsync(id);
        }

        public async Task<ActionResult> Create(Message message)
        {
            await _db.Messages.AddAsync(message);
            return new HttpOk().ToJson();
        }

        public async Task<ActionResult> Delete(long id)
        {
            var message = await _db.Messages.FindAsync(id);
            
            if (message == null) return new NotFoundHttpException(id).ToJson();
            
            _db.Messages.Remove(message);
            return new HttpOk().ToJson();
        }

        public async Task<ActionResult<Chat>> Chat(long id)
        {
            var message = await _db.Messages.FindAsync(id);
            if ( message == null ) return new NotFoundHttpException(id).ToJson();
            return message.Chat;
        }

        public async Task<ActionResult<User>> User(long id)
        {
            var message = await _db.Messages.FindAsync(id);
            if ( message == null ) return new NotFoundHttpException(id).ToJson();
            return message.User;
        }
    }
}