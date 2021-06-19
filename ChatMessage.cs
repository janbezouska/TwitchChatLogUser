using System;

namespace TwitchChatLogUser
{
  public class Message
  {
    public string Channel { get; set; }
    public string Username { get; set; }
    public string ChatMessage { get; set; }
    public DateTime TimeStamp { get; set; }
    public int ID { get; set; }
  }
}
