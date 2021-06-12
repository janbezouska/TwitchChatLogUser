using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using Dapper;

namespace TwitchChatLogUser
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      cbChannel.Items.Add("Herdyn");
    }

    private void butSearch_Click(object sender, RoutedEventArgs e)
    {
      if (tbLog.Text == string.Empty)
        tbLog.Text = "tady se budou vypisovat chyby";
      else
        tbLog.Text = string.Empty;

      tbMessages.Text += DateTime.Now.ToString("dd.MM. yy HH:mm") + " Username: nějaká průměrně dlouhá zpráva :) xd \n";
      bool err = false;
      if (err)
      {

      }
      else
      {
        foreach (var message in GetMessages(cbChannel.Text, tbUsername.Text))
        {
          tbMessages.Text += message.When.ToString("dd.MM yy (HH:mm): " + message.Message + "\n");
        }
      }
    }

    private List<ChatMessage> GetMessages(string channel, string username)
    {
      SqlConnectionStringBuilder builder = new();

      builder.DataSource = "sql-twitchchatlogger.database.windows.net";
      builder.UserID = "defaultUser";
      builder.Password = "SuperSecret!";
      builder.InitialCatalog = "ChatLogs";

      using (SqlConnection connection = new(builder.ConnectionString))
      {
        //return connection.Query<ChatMessage>($"SELECT * FROM ChatLogs WHERE Channel = '{channel.ToLower()}' AND Username = '{username}'").AsList();
        List<ChatMessage> msgs = connection.Query<ChatMessage>("SELECT Channel, Username, ChatMessage, TimeStamp FROM ChatLogs").AsList();
        return msgs;
      }
    }
  }
}
