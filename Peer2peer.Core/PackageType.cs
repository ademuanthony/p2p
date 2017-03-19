using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer
{
    public class PackageType:Entity
    {
        public const string Basic = "basic";
        public const string Bronze = "bronze";
        public const string Silver = "silver";
        public const string Gold = "gold";

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double ReturnOnInvestment { get; set; }
        public int LifeSpan { get; set; }
    }
}
