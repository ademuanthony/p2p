using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Peer2peer.EntityFramework.Repositories
{
    public abstract class Peer2peerRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<Peer2PeerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected Peer2peerRepositoryBase(IDbContextProvider<Peer2PeerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class Peer2peerRepositoryBase<TEntity> : Peer2peerRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected Peer2peerRepositoryBase(IDbContextProvider<Peer2PeerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
