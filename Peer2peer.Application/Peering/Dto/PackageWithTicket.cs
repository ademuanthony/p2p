using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Peering.Dto
{
    public class PackageWithTicket
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Amount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
