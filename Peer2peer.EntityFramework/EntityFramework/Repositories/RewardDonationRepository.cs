using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Peer2peer.Dos;

namespace Peer2peer.EntityFramework.Repositories
{
    public interface IRewardDonationRepository:IRepository<ReferralRewardDonation>
    {
        List<Transaction> GetTransactions(long userId);
        List<Transaction> GetPendingConfirmations(long userId);
    }

    public class RewardDonationRepository : Peer2peerRepositoryBase<ReferralRewardDonation>, IRewardDonationRepository
    {
        public RewardDonationRepository(IDbContextProvider<Peer2PeerDbContext> provider):base(provider)
        {

        }

        public List<Transaction> GetTransactions(long userId)
        {
            var query = from
                        d in Context.ReferralRewardDonations join 
                        p in Context.Packages on d.Id equals p.Id select new { d, p };

            var result = query.Where(q => q.d.Status == Status.PaidOut && (q.d.ToUserId == userId))
                .Select(v => new Transaction
                {
                    ToName = v.d.ToUser.Name + " " + v.d.ToUser.Surname,
                    FromName = v.p.User.Name + " " + v.p.User.Surname,
                    Amount = v.d.Amount,
                    Date = v.d.Date,
                    FromPhoneNumber = v.p.User.PhoneNumber,
                    ToPhoneNumber = v.d.ToUser.PhoneNumber,
                    DonationId = v.d.Id,
                    Status = v.d.Status,
                    IsIncoming = userId == v.d.ToUserId.Value
                });

            return result.ToList();
        }

        public List<Transaction> GetPendingConfirmations(long userId)
        {
            var query = from
                        d in Context.ReferralRewardDonations join 
                        p in Context.Packages on d.PackageId equals p.Id select new { donation = d, package = p };
            var result = query.Where(q => q.donation.ToUserId == userId
                                          && (q.donation.Status == Status.Pending ||
                                              q.donation.Status == Status.AwaitingAlert))
                .OrderBy(q => q.package.CreatedDate)
                .Select(q => new Transaction
                {
                    ToName = q.donation.ToUser.Name + " " + q.donation.ToUser.Surname,
                    FromName = q.package.User.Name + " " + q.package.User.Surname,
                    Amount = q.donation.Amount,
                    Date = q.donation.Date,
                    FromPhoneNumber = q.package.User.PhoneNumber,
                    ToPhoneNumber = q.donation.ToUser.PhoneNumber,
                    DonationId = q.donation.Id,
                    Status = q.donation.Status,
                    IsIncoming = userId == q.donation.ToUserId.Value
                });

            return result.ToList();
        }
    }
}
