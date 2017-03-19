using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Peer2peer
{
    public class DonationTicket:Entity
    {
        public long UserId { get; set; }
        public int PackageId { get; set; }
        public int? DonationId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }

       // public Package Package { get; set; }
    }
}
