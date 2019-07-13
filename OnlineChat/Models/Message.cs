using System;

namespace OnlineChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CTime { get; set; }
        public int UserId { get; set; }
        public string ChatName { get; set; }
        
        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}