using Abp.Web.Mvc.Views;

namespace Peer2peer.Web.Views
{
    public abstract class Peer2peerWebViewPageBase : Peer2peerWebViewPageBase<dynamic>
    {

    }

    public abstract class Peer2peerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected Peer2peerWebViewPageBase()
        {
            LocalizationSourceName = Peer2peerConsts.LocalizationSourceName;
        }
    }
}