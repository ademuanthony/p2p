using Abp.Domain.Entities;
using Peer2peer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Timing;

namespace Peer2peer
{
    public class Donation:Entity
    {
        public int TicketId { get; set; }
        public int PackageId { get; set; }
        public long FromUserId { get; set; }
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
