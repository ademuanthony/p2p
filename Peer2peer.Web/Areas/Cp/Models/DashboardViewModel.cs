using System;
using Peer2peer.Dos;
using System.Collections.Generic;
using System.Linq;

namespace Peer2peer.Web.Areas.Cp.Models
{
    public class DashboardViewModel
    {
        public bool CircleCompleted => PendingConfirmations != null && PendingConfirmations.Count == 0 && PendingPackage == null;

        public List<Transaction> PendingConfirmations { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Donation PendingDonation { get; set; }
        public Package PendingPackage { get; set; }
        public Dictionary<string, PackageType> PackageTypes { get; set; }

        public double TotalDonationMade { get { return Transactions.Where(t => !t.IsIncoming).Sum(t => t.Amount); } }
        public double TotalDonatioReceived { get { return Transactions.Where(t => t.IsIncoming).Sum(t => t.Amount); } }
        public double Profit => TotalDonatioReceived - TotalDonationMade > 0 ? TotalDonatioReceived - TotalDonationMade:0;

        public int GetPercentage(string item)
        {
            var totalMoney = Transactions.Sum(t => t.Amount);
            if (Math.Abs(totalMoney) < 0.99) return 0;
            switch (item)
            {
                case "tdm":
                    return (int)((TotalDonationMade / totalMoney) * 100);
                case "tdr":
                    return (int)((TotalDonatioReceived / totalMoney) * 100);
                case "pft":
                    return (int)((Profit / totalMoney) * 100);
            }
            return 0;
        }
    }
}