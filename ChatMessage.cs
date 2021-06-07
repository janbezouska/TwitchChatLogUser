using System;

namespace TwitchChatLogger
{
  public class ChatMessage
  {
    public DateTime When { get; set; }
    public string Channel { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
  }
}
