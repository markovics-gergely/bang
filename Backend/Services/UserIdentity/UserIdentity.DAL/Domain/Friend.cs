using System;

using Microsoft.AspNetCore.Identity;

namespace UserIdentity.DAL.Domain
{
    public class Friend : IEquatable<Friend>
    {
        public long Id { get; set; }
        public string SenderId { get; set; }
        public IdentityUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public IdentityUser Receiver { get; set; }

        public bool Equals(Friend friend)
        {
            return friend.SenderId == SenderId && friend.ReceiverId == SenderId;
        }
    }
}
