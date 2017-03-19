using Abp.Domain.Entities;
using Peer2peer.Users;
using System;
using System.Collections.Generic;

namespace Peer2peer
{
    public class Package:Entity
    {
        public int TypeId { get; set; }
        public long UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public double ExpectedRi { get; set; }
        public double CurrentRi { get; set; }

        public PackageType Type { get; set; }
        public User User { get; set; }
        //public Donation Donation { get; set; }

        public ICollection<DonationTicket> Tickets { get; set; }
    }
}
