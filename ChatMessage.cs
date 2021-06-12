using System;

namespace TwitchChatLogUser
{
  public class ChatMessage
  {
    public string Channel { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public DateTime When { get; set; }
  }
}
