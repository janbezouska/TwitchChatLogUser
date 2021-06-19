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

      tbLog.Text = "Searching will take a few seconds! \n(be patient)";
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
          tbMessages.Text += message.TimeStamp.ToString("dd.MM yy (HH:mm): ") + message.ChatMessage + "\n";
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
        List<Message> messages = db.GetMessages(cbChannel.Text, tbUsername.Text);

        if (messages.Count == 0)
          tbLog.Text = "Nenalezeny žádné zprávy od uživatele na vybraném kanále";

        foreach (Message message in messages)
          tbMessages.Text += message.TimeStamp.ToString("dd.MM yy (HH:mm): ") + message.ChatMessage + Environment.NewLine;
      }
    }

    private void butRand_Click(object sender, RoutedEventArgs e)
    {
      tbMessages.Text = string.Empty;

      List<Message> messages = db.RandomUser(cbChannel.Text);

      foreach (Message message in messages)
        tbMessages.Text += message.Username.Trim() + ": " + message.TimeStamp.ToString("dd.MM yy (HH:mm): ") + message.ChatMessage + Environment.NewLine + Environment.NewLine;
    }
  }

  public class ReadDb
  {
    private SqlConnectionStringBuilder builder = new();
    private Random rnd = new();

    public ReadDb()
    {
      builder.DataSource = "sql-twitchchatlogger.database.windows.net";
      builder.UserID = "defaultUser";
      builder.Password = "SuperSecret!";
      builder.InitialCatalog = "ChatLogs";
    }

    public List<Message> GetMessages(string channel, string username)
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        return connection.Query<Message>($"SELECT * FROM ChatLogs WHERE Channel = '{channel.ToLower()}' AND Username = '{username}'").AsList();;
      }
    }

    public List<string> GetChannels()
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        return connection.Query<string>("SELECT Channel FROM ChatLogs").AsList();
      }
    }

    public List<Message> RandomUser(string channel)
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        List<string> allUsers = connection.Query<string>($"SELECT Username FROM ChatLogs WHERE Channel = '{channel}'").AsList();
        string user = allUsers[rnd.Next(0, allUsers.Count)];

        return connection.Query<Message>($"SELECT * FROM ChatLogs WHERE Channel = '{channel.ToLower()}' AND Username = '{user}'").AsList();
      }
    }

    //for debugging
    public List<Message> GetAllMessages(string channel)
    {
      using (SqlConnection connection = new(builder.ConnectionString))
      {
        return connection.Query<Message>("SELECT * FROM ChatLogs").AsList();
      }
    }
  }
}
