using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Timing;
using Peer2peer.Users;

namespace Peer2peer
{
    public class ReferralRewardDonation:Entity
    {
        /// <summary>
        /// The reward ticket that owns this donation
        /// Unique
        /// </summary>
        public int TicketId { get; set; }
        /// <summary>
        /// The package that is making this donation
        /// </summary>
        public int PackageId { get; set; }
        /// <summary>
        /// The id of the user that is making the donation
        /// </summary>
        public long FromUserId { get; set; }
        /// <summary>
        /// The user that the donation is going to
        /// </summary>
        public long? ToUserId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }

        public DateTime PenaltyDateTime => Date.AddHours(6);

        public string TimeLeft
        {
            get
            {
                var timeStamp = PenaltyDateTime - Clock.Now;
                return $"{timeStamp.Hours} hours & {timeStamp.Minutes} munites";
            }
        }

        /*[ForeignKey("Id")]
                public Package Package { get; set; }*/
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
    }
}
