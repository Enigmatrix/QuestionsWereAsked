using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
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
using MaterialDesignThemes.Wpf.Transitions;
using QuestionsWereAsked.Common;
using QuestionsWereAsked.Common.Messages;

namespace QuestionsWereAsked.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            /*
            var client = new TcpClient("127.0.0.1", Config.Port);
            var socket = client.Client;
            var secure = socket.ReceiveRSA<SecureMessage>();
            //var sendRsa = RSA.Create();
            //sendRsa.ImportParameters(secure.ToRsaParameters());


            //var recvRsa = Config.CreateRsa();
            //var param = recvRsa.ExportParameters(false);
            var secureReply = new SecureReplyMessage("BIGDICK");

            socket.SendRSA(secureReply);
            var msg = socket.ReceiveRSA<GameStartMessage>();
            MessageBox.Show(msg.Player2NickName + ", "+msg.Player1NickName);*/
        }
    }
}
