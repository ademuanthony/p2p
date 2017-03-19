using Peer2peer.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Peer2peer.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly Peer2PeerDbContext _context;

        public InitialHostDbBuilder(Peer2PeerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            new PeerSeeder(_context).CreatePackeTypes();
        }
    }
}
