using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Peer2peer.Dos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.EntityFramework.Repositories
{
    public interface IDonationRepository:IRepository<Donation>
    {
        List<Transaction> GetTransactions(long userId);
        List<Transaction> GetPendingConfirmations(long userId);
    }

    public class DonationRepository:Peer2peerRepositoryBase<Donation>, IDonationRepository
    {
        public DonationRepository(IDbContextProvider<Peer2PeerDbContext> provider):base(provider)
        {

        }

        public List<Transaction> GetTransactions(long userId)
        {
            var query = from d in Context.Donations join p in Context.Packages on d.Id equals p.Id select new {d, p};
            var result = query.Where(q => q.d.Status == Status.PaidOut && (q.d.FromUserId == userId || q.d.ToUserId == userId))
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
            var query = from d in Context.Donations join p in Context.Packages on d.PackageId equals p.Id select new { donation= d, package=p };
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
