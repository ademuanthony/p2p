namespace Peer2peer.Web.Models.Layout
{
    public class AlertViewModel
    {
        public const string Key = "alerts";
        public const string Success = "success";
        public const string Error = "error";
        public const string Info = "info";

        public string Title { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public string Layout => Type == Success ? "top" : "bottom";
    }
}