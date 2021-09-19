using System;

using Microsoft.AspNetCore.Identity;

namespace UserIdentity.DAL.Domain
{
    public class Friend : IEquatable<Friend>
    {
        public long Id { get; set; }
        public string SenderId { get; set; }
        public Account Sender { get; set; }
        public string ReceiverId { get; set; }
        public Account Receiver { get; set; }
        public bool isInvitedToGame { get; set; }

        public bool Equals(Friend friend)
        {
            return friend.SenderId == ReceiverId && friend.ReceiverId == SenderId;
        }

        public Friend Switch()
        {
            var tempReceiverId = this.SenderId;
            var tempReceiver = this.Sender;

            return new Friend
            {
                Id = this.Id,
                SenderId = this.ReceiverId,
                Sender = this.Receiver,
                ReceiverId = tempReceiverId,
                Receiver = tempReceiver
            };
        }
    }
}
