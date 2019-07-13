using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;

namespace OnlineChat.Hubs
{
    public class ChatHub: Hub
    {
        private IChatService _chatService;
        private IMessageService _messageService;
        
        public ChatHub(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        public Task Send(Message message)
        {
            if ( _chatService.Get(message.ChatName) == null)
            {
                throw new System.Exception("cannot send a message item to a chat which does not exist.");    
            }

            _messageService.Create(message);
            return Clients.Group(message.Chat.Name).SendAsync("Send", message);
        }

        public async Task JoinChat(string chatName)
        {
            if ( _chatService.Get(chatName) == null)
            {
                throw new System.Exception("cannot join a chat which does not exist.");    
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
            await Clients.Group(chatName).SendAsync("JoinGroup", chatName);
            
            var history = _chatService.Messages(chatName);
            await Clients.Client(Context.ConnectionId).SendAsync("History", history);
        }

        public async Task LeaveChat(string chatName)
        {
            if ( _chatService.Get(chatName) == null)
            {
                throw new System.Exception("cannot leave a chat which does not exist.");    
            }
            
            await Clients.Group(chatName).SendAsync("LeaveGroup", chatName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        }
    }
}