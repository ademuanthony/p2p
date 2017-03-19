using System.Linq;
using Peer2peer.EntityFramework;
using Peer2peer.MultiTenancy;

namespace Peer2peer.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly Peer2PeerDbContext _context;

        public DefaultTenantCreator(Peer2PeerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
