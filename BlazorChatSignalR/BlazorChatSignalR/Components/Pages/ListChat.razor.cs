namespace BlazorChatSignalR.Components.Pages
{
	public partial class ListChat
	{
		string nameAccount = "";
		string lastTime = DateTime.Now.ToString("HH:mm");
		string lastMessage = "";

		public List<Chatroom> ChatList { get; set; } = new List<Chatroom>();
	}

	public class Chatroom
	{
		public string nameAccount { get; set; }
		public string lastTime { get; set; }
		public string lastMessage { get; set; }
	}
}