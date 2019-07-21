using System.Collections;
using System.Collections.Generic;
using OnlineChat.Models;

namespace OnlineChat.ViewModels
{
    public class MessageHistoryVm
    {
        public ICollection<Message> Messages { get; set; }
    }
}