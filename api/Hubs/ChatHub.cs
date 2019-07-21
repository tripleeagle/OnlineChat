using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineChat.Models;
using OnlineChat.Services.Interfaces;
using OnlineChat.ViewModels;

namespace OnlineChat.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        private const string NewMessageNotification = "NewMessageNotification";
        private const string UserJoinChatNotification = "UserJoinChatNotification";
        private const string UserLeaveChatNotification = "UserLeaveChatNotification";
        private const string NewHistoryNotification = "NewHistoryNotification";
        
        
        public ChatHub(IChatService chatService, IMessageService messageService, IUserService userService)
        {
            _chatService = chatService;
            _messageService = messageService;
            _userService = userService;
        }

        public async Task SendToAll(string chatName, string userName, string messageText)
        { 
            //Clients.All.SendAsync("sendToAll", name, message);
            var chat = await _chatService.Get(chatName);
            var user = _userService.GetByName(userName);
            if (chat == null)
            {
                throw new System.Exception("cannot send a message item to a chat which does not exist.");    
            }

            if (user == null)
            {
                throw new System.Exception("cannot a message with undefined origin.");    
            }

            var message = new Message
            {
                Chat = chat,
                CTime = DateTime.Now,
                Text = messageText,
                User = user
            };
            
            await _messageService.Create(message);
            await Clients.Group(chatName).SendAsync(NewMessageNotification, messageText);
        }

        public async Task JoinChat(string userName, string chatName)
        {
            var chat = await _chatService.Get(chatName);
            var user = _userService.GetByName(userName);
            if ( chat == null)
            {
                throw new System.Exception("cannot join a chat which does not exist.");    
            }
            if (user == null)
            {
                throw new System.Exception("an unknown user cannot join a chat");   
            }
            
            var history = await _chatService.Messages(chatName);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
            await Clients.Group(chatName).SendAsync(UserJoinChatNotification, userName);
            
            var messageHistoryVm = JsonConvert.SerializeObject(new MessageHistoryVm{Messages = history}, Formatting.None,
            new JsonSerializerSettings()
            { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await Clients.Client(Context.ConnectionId).SendAsync(NewHistoryNotification, messageHistoryVm);
        }

        public async Task LeaveChat(string userName, string chatName)
        {
            if ( _chatService.Get(chatName) == null)
            {
                throw new System.Exception("cannot leave a chat which does not exist.");    
            }
            
            await Clients.Group(chatName).SendAsync(UserLeaveChatNotification, chatName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        }
    }
}