using System;

namespace UserIdentity.DAL.Domain
{
    public class Message
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public DateTimeOffset PostedDate { get; set; }
    }
}
