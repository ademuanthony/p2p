using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Peer2peer
{
    public class ReferralRewardTicket:Entity
    {
        /// <summary>
        /// the benefiting user id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// the donation of the downline that was confirmed to warrant this ticket. it is unique
        /// </summary>
        public int DonationId { get; set; }

        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }
    }
}
