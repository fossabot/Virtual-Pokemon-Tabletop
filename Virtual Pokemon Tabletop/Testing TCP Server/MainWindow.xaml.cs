﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssaultBird2454.VPTU.Networking.Server.TCP;
using System.Reflection;
using System.Collections.ObjectModel;
using AssaultBird2454.VPTU.Networking.Server.Command_Handeler;
using System.Security.Cryptography.X509Certificates;

namespace Testing_TCP_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TCP_Server Server;
        private ObservableCollection<TCP_ClientNode> ClientList { get; set; }
        Server_CommandHandeler cmdhand;

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            cmdhand = new Server_CommandHandeler();

            cmdhand.RegisterCommand<MessageData>("Message", CMDHandel);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientList = null;
                Server.Stop();
                Server = null;
            }
            catch { }
            try
            {
                ClientList = new ObservableCollection<TCP_ClientNode>();
                Clients.ItemsSource = ClientList;

                Server = new TCP_Server(IPAddress.Any, cmdhand, 25444);
                try
                {
                    Server.SSL_Cert = X509Certificate.CreateFromCertFile(AssemblyDirectory + "/Cert.cer");
                }
                catch { MessageBox.Show("Server Has No Certificate"); }
                Server.MaxConnections = 10;

                Server.TCP_ClientState_Changed += Server_TCP_ClientState_Changed;
                Server.TCP_AcceptClients_Changed += new TCP_AcceptClients_Handeler((s) => AcceptConnection.IsChecked = s);
                Server.TCP_Data_Event += new TCP_Data((s, c, d) =>
                {
                    Action act = new Action(() => MessageBox.Show("Data Recv\n\nClient ID: " + c.ID + "\nData Direction: " + d.ToString() + "\nData: " + s));
                    act.Invoke();
                });

                Server.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Start\n\n" + ex.ToString());
            }
        }

        private void CMDHandel(object Data, TCP_ClientNode Client)
        {
            Console.Dispatcher.Invoke(new Action(() => Console.AppendText("\n[" + DateTime.Now.ToShortTimeString() + "] -> " + ((MessageData)Data).Message)));
        }

        private void Server_TCP_ClientState_Changed(AssaultBird2454.VPTU.Networking.Server.TCP.TCP_ClientNode Client, AssaultBird2454.VPTU.Networking.Data.Client_ConnectionStatus Client_State)
        {
            if (Client_State == AssaultBird2454.VPTU.Networking.Data.Client_ConnectionStatus.Connected)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.ClientList.Add(Client);
                }));
            }
            else if (Client_State == AssaultBird2454.VPTU.Networking.Data.Client_ConnectionStatus.Disconnected)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    try { this.ClientList.Remove(Client); } catch { }
                }));
            }
            else if (Client_State == AssaultBird2454.VPTU.Networking.Data.Client_ConnectionStatus.Rejected)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    try { this.ClientList.Remove(Client); } catch { }
                }));
            }

            //Clients.Dispatcher.Invoke(new Action(() => Clients.Items.Refresh()));
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.Stop();
                Server = null;
            }
            catch { }
        }

        private void AcceptConn_Click(object sender, RoutedEventArgs e)
        {
            Server.AcceptClients = (bool)AcceptConnection.IsChecked;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Server.Client_SendData(new MessageData(Data.Text));
        }

        private void Send10_Click(object sender, RoutedEventArgs e)
        {
            MessageData DataObj = new MessageData(Data.Text);

            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
        }
    }

    public class MessageData : AssaultBird2454.VPTU.Networking.Data.NetworkCommand
    {
        public string Command
        {
            get
            {
                return "Message";
            }
        }

        public MessageData(string _Message)
        {
            Message = _Message;
        }

        public string Message;
    }
}
