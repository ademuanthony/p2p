using System;
using Peer2peer.Dos;
using System.Collections.Generic;
using System.Linq;
using Peer2peer.Users;

namespace Peer2peer.Web.Areas.Cp.Models
{
    public class DashboardViewModel
    {
        public User CurrentUser { get; set; }

        public bool CircleCompleted => PendingConfirmations != null &&
                                       PendingConfirmations.Count == 0 && PendingPackage == null &&
                                       PendingRewardDonation == null;

        public List<Transaction> PendingConfirmations { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Donation PendingDonation { get; set; }
        public ReferralRewardDonation PendingRewardDonation { get; set; }
        public Package PendingPackage { get; set; }
        public Dictionary<string, PackageType> PackageTypes { get; set; }

        public double TotalDonationMade { get { return Transactions.Where(t => !t.IsIncoming).Sum(t => t.Amount); } }
        public double TotalDonatioReceived { get { return Transactions.Where(t => t.IsIncoming).Sum(t => t.Amount); } }
        public double Profit => TotalDonatioReceived - TotalDonationMade > 0 ? TotalDonatioReceived - TotalDonationMade:0;

        public List<User> Downlines { get; set; }
        public List<Transaction> ReferralPendingConfirmations { get; set; }

    

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