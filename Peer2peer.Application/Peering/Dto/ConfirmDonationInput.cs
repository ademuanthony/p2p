using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Peering.Dto
{
    public class ConfirmDonationInput
    {
        public long CurrentUserId { get; set; }
        public int DonationId { get; set; }
    }
}
