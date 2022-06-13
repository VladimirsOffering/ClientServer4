using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using Client;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для ServersList.xaml
    /// </summary>
    /// 
    public partial class ServersList : Window
    {
        private ViewModel viewModel;
        Socket tcpSocket;
        public ServersList()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            this.viewModel = new ViewModel
            {
                Servers = new List<DataGridItem>()
            };
            this.DGContent.DataContext = viewModel;
            viewModel.Servers.Add(new DataGridItem("192.168.30.160", "1234"));
            viewModel.Servers.Add(new DataGridItem("192.168.30.183", "1234"));
            viewModel.Servers.Add(new DataGridItem("192.168.30.163", "1234"));
        }

        private void UpdateServersList()
        {


            const string ip = "192.168.0.102";
            const int port = 1234;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string Message = "qweqwe";
            var data = Encoding.UTF8.GetBytes(Message);

            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(BitConverter.GetBytes(data.Length));
            tcpSocket.Send(data);
            tcpSocket.Close();


            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //socket.Bind(new IPEndPoint(IPAddress.Any, 8002));
            //socket.Connect(new IPEndPoint(IPAddress.Broadcast, 8001));
            //socket.Send(System.Text.ASCIIEncoding.UTF8.GetBytes("hello"));

            //int availableBytes = socket.Available;
            //if (availableBytes > 0)
            //{
            //    byte[] buffer = new byte[availableBytes];
            //    socket.Receive(buffer, 0, availableBytes, SocketFlags.None);
            //    // buffer has the information on how to connect to the server
            //}
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateServersList();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = DGContent.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataGridItem server = viewModel.Servers.ToList()[index];
            MessageBox.Show(server.IP);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tcpSocket.Close();
        }
    }
}
