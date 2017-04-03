using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Peering.Dto
{
    public class ChangeDonationStatusInput
    {
        [Required]
        public int DonationId { get; set; }
        [Required]
        public long CurrentUserId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
