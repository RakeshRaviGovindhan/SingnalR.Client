
namespace SignalRVideoCall.Model
{
    public class MessageModel
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool IsOwnerMessage { get; set; }
    }
}
