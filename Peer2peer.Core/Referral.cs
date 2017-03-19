using Abp.Domain.Entities;
using Peer2peer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer
{
    public class Referral:Entity
    {
        public long UserId { get; set; }
        public long DownlineId { get; set; }

        public User User { get; set; }

        [ForeignKey("DownlineId")]
        public User Downline { get; set; }
    }
}
