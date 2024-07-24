namespace BlazorChatSignalR.Client.Pages
{
    public class Chat
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
