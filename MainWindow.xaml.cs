using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace TwitchChatLogUser
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private ReadDb db;

    public MainWindow()
    {
      InitializeComponent();
      db = new();

      List<string> channelsToAdd = new();
      foreach (string channel in db.GetChannels())
      {
        if (!channelsToAdd.Contains(channel.Trim()))
          channelsToAdd.Add(channel.Trim());
      }

      foreach (string channel in channelsToAdd)
      {
        cbChannel.Items.Add(channel);
      }

      cbChannel.SelectedIndex = 1;
    }

    private void butSearch_Click(object sender, RoutedEventArgs e)
    {
      tbMessages.Text = string.Empty;

      if (string.IsNullOrEmpty(tbUsername.Text))
      {
        tbLog.Text = "Prosím zadej username";
      }

      //all messages from one channel
      else if (tbUsername.Text == "*")
      {
        foreach (var message in db.GetAllMessages(cbChannel.Text))
        {
          tbMessages.Text += message.When.ToString("dd.MM yy (HH:mm): ") + message.Message + "\n";
        }
      }
      else if (tbUsername.Text.Length > 25)
      {
        tbLog.Text = "Username je moc dlouhý - tento uživatel neexistuje (max 25 znaků)";
      }
      else if (tbUsername.Text.Length < 3)
      {
        tbLog.Text = "Username je moc krátký - tento uživatel neexistuje (min 3 znaky)";
      }
      else
      {
        List<ChatMessage> messages = db.GetMessages(cbChannel.Text, tbUsername.Text);

        if (messages.Count == 0)
        {
          tbLog.Text = "Nenalezeny žádné zprávy od uživatele na vybraném kanále";
        }

        foreach (ChatMessage message in messages)
        {
          tbMessages.Text += message.When.ToString("dd.MM yy (HH:mm): ") + message.Message + "\n";
        }
      }
    }
  }

  public class ReadDb
  {
    private SqlConnectionStringBuilder builder = new();

    public ReadDb()
    {
      builder.DataSource = "sql-twitchchatlogger.database.windows.net";
      builder.UserID = "defaultUser";
      builder.Password = "SuperSecret!";
      builder.InitialCatalog = "ChatLogs";
    }

    public List<ChatMessage> GetMessages(string channel, string username)
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        List<String> messages = connection.Query<String>($"SELECT ChatMessage FROM ChatLogs WHERE Channel = '{channel.ToLower()}' AND Username = '{username}'").AsList();
        List<DateTime> timeStamps = connection.Query<DateTime>($"SELECT TimeStamp FROM ChatLogs WHERE Channel = '{channel.ToLower()}' AND Username = '{username}'").AsList();

        List<ChatMessage> chatMessages = new();

        foreach (var message in messages.Zip(timeStamps, Tuple.Create))
        {
          chatMessages.Add(new ChatMessage
          {
            Channel = channel,
            Name = username,
            Message = message.Item1,
            When = message.Item2
          });
        }
        return chatMessages;
      }
    }

    public List<string> GetChannels()
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        return connection.Query<string>("SELECT Channel FROM ChatLogs").AsList();
      }
    }

    //for debugging
    public List<ChatMessage> GetAllMessages(string channel)
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        List<String> messages = connection.Query<String>($"SELECT ChatMessage FROM ChatLogs WHERE Channel = '{channel.ToLower()}'").AsList();
        List<DateTime> timeStamps = connection.Query<DateTime>($"SELECT TimeStamp FROM ChatLogs WHERE Channel = '{channel.ToLower()}'").AsList();

        List<ChatMessage> chatMessages = new();

        foreach (var message in messages.Zip(timeStamps, Tuple.Create))
        {
          chatMessages.Add(new ChatMessage
          {
            Message = message.Item1,
            When = message.Item2
          });
        }
        return chatMessages;
      }
    }
  }
}
