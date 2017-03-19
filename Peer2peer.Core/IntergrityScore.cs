using Abp.Domain.Entities;
using Peer2peer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer
{
    public class IntergrityScore:Entity
    {
        public int UserId { get; set; }
        public string Source { get; set; }
        public int Score { get; set; }

        public User User { get; set; }
    }
}
