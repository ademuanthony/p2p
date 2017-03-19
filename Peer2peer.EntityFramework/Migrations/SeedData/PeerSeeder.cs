using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peer2peer.EntityFramework;

namespace Peer2peer.Migrations.SeedData
{
    public class PeerSeeder
    {
        private readonly Peer2PeerDbContext _context;

        public PeerSeeder(Peer2PeerDbContext context)
        {
            _context = context;
        }

        public void CreatePackeTypes()
        {
            if (!_context.PackageTypes.Any(p => p.Name == PackageType.Basic))
            {
                _context.PackageTypes.Add(new PackageType
                {
                    Name = PackageType.Basic,
                    Description = PackageType.Basic,
                    LifeSpan = 6,
                    Price = 5000,
                    ReturnOnInvestment = 10000
                });
            }


            if (!_context.PackageTypes.Any(p => p.Name == PackageType.Bronze))
            {
                _context.PackageTypes.Add(new PackageType
                {
                    Name = PackageType.Bronze,
                    Description = PackageType.Bronze,
                    LifeSpan = 6,
                    Price = 10000,
                    ReturnOnInvestment = 20000
                });
            }

            if (!_context.PackageTypes.Any(p => p.Name == PackageType.Silver))
            {
                _context.PackageTypes.Add(new PackageType
                {
                    Name = PackageType.Silver,
                    Description = PackageType.Silver,
                    LifeSpan = 6,
                    Price = 20000,
                    ReturnOnInvestment = 40000
                });
            }

            if (!_context.PackageTypes.Any(p => p.Name == PackageType.Gold))
            {
                _context.PackageTypes.Add(new PackageType
                {
                    Name = PackageType.Gold,
                    Description = PackageType.Gold,
                    LifeSpan = 6,
                    Price = 50000,
                    ReturnOnInvestment = 100000
                });
            }
        }
    }
}
