using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Dos
{
    public class Transaction
    {
        public int DonationId { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string FromPhoneNumber { get; set; }
        public string ToPhoneNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public string Status { get; set; }
        public bool IsIncoming { get; set; }
    }
}
