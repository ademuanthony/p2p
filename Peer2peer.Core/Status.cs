using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer
{
    public class Status
    {
        public const string Active = "Active";
        public const string InActive = "In Active";
        public const string Removed = "Removed";
        public const string InVisible = "In Visible";

        public const string Pending = "Pending"; //Your package have not been paired by the system
        public const string Paired = "Paired"; //Your package have been paired but you have not made payment
        public const string AwaitingAlert = "Awaiting Alert";//You told your beneficiary that you have paid but he said he is waiting for alert
        public const string Confirm = "Confirm";//Your beneficiary have confirmed your payment
        public const string Rejected = "Rejected";//Your payment claim your rejected

        public const string TicketsCreated = "Tickets Created";
        public const string AwaitingPayment = "Awaiting Payment";//the user have been paired, not all have paid
        public const string PaidOut = "Paid Out";//you have been paired and paid
        public const string UrgentPairingNeeded = "Urgent Pairing Needed";

    }
}
