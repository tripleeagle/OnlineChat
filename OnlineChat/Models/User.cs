using System.Collections;
using System.Collections.Generic;

namespace OnlineChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatId { get; set; }
        
        public virtual Chat Chat { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}