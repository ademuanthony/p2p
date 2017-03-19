using System.Data.Common;
using Abp.Zero.EntityFramework;
using Peer2peer.Authorization.Roles;
using Peer2peer.MultiTenancy;
using Peer2peer.Users;
using System.Data.Entity;

namespace Peer2peer.EntityFramework
{
    public class Peer2PeerDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public Peer2PeerDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in Peer2peerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of Peer2peerDbContext since ABP automatically handles it.
         */
        public Peer2PeerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public Peer2PeerDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Donation>()
                .HasRequired(d => d.Package)
                .WithOptional();*/

            modelBuilder.Entity<Donation>().HasRequired(d=>d.ToUser).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Referral>().HasRequired(r=>r.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Referral>().HasRequired(r => r.User).WithOptional();


        }


        public IDbSet<Donation> Donations { get; set; }
        public IDbSet<IntergrityScore> IntergrityScores { get; set; }
        public IDbSet<Package> Packages { get; set; }
        public IDbSet<PackageType> PackageTypes { get; set; }
        public IDbSet<Referral> Referrals { get; set; }
        public IDbSet<DonationTicket> DonationTickets { get; set; }
    }
}
