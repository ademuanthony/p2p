using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2peer.Configuration
{
    public interface IConfigurationSErvice
    {
        bool GetAutoMergingValue();

    }

    public class ConfigurationService:Peer2peerAppServiceBase, IConfigurationSErvice
    {
        public bool GetAutoMergingValue()
        {
            return true;
        }
    }
}
