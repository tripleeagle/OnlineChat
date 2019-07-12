using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Services
{
    public class MessageService: IMessageService
    {
        public Task<ActionResult<List<Message>>> All()
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Message>> Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Create(Message question)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Message>> Message(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<Message>> User(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}