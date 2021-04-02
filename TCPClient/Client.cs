using java.lang;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private StringBuffer _receiveContent =new StringBuffer();

        private Socket _clientSockect;

        private bool _isStop = false;

        private bool _isSussessConnected = false;

        private delegate void SendDelegate(string text);

        private delegate void ReceiveDelegate();

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkServer_Button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ClientIP_TextBox.Text))
            {
                MessageBox.Show("客户端IP为空");
                return;
            }
            if(string.IsNullOrEmpty(ClientPort_TextBox.Text))
            {
                MessageBox.Show("客户端端口为空");
                return;
            }
            IPAddress iPAddress = IPAddress.Parse(ClientIP_TextBox.Text.Trim());
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, int.Parse(ClientPort_TextBox.Text.Trim()));
            _clientSockect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ShowMessage("client:尝试连接服务器" + "\r\n");
                _clientSockect.Connect(iPEndPoint);
            }
            catch(SocketException se)
            {
                ShowMessage("  "+se.Message+"\r\n");
                Receive_RichTextBox.Text = _receiveContent.ToString();
                return;
            }     
            _isSussessConnected = true;
            LinkServer_Button.Enabled = false;
            Send_Button.Enabled = true;
            Task.Run(() =>
            {
                ReceiveMessage();
            });
            ShowMessage("连接服务器成功" + "\r\n");
            //Receive();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Send_RichTextBox.Text))
            {
                MessageBox.Show("发送消息为空");
                return;
            }
            if(!_isSussessConnected)
            {
                MessageBox.Show("未成功连接服务器，无法发送消息");
                return;
            }
            string message = $"client:\r\n-->   {Send_RichTextBox.Text}\r\n";
            Task.Run(() =>
            {
                SendMessage(message);
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                byte[] sendMessage = new byte[data.Length + 1];
                // 用来表示发送的是消息数据
                sendMessage[0] = 0;
                Buffer.BlockCopy(data, 0, sendMessage, 1, data.Length);
                _clientSockect.Send(sendMessage);
                ShowMessage(message);
            }
            catch(SocketException se)
            {
                ShowMessage("发送失败");
            }
            finally
            {
                Send_RichTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void ReceiveMessage()
        {
            while(!_isStop)
            {
                byte[] data = new byte[1024 * 1024 * 2];
                int length = -1;
                try
                {
                    length = _clientSockect.Receive(data);                 
                }
                catch(SocketException se)
                {
                    ShowMessage(se.Message + "\r\n");
                }
                if (length != 0)
                {
                    if (data[0] == 0)
                    {
                        string message = Encoding.UTF8.GetString(data, 1, data.Length - 1).Trim().Replace("\0", string.Empty);
                        ShowMessage(message);
                    }
                }
            }
        }



        private void ShowMessage(string message)
        {
            _receiveContent.append(message);
            Receive_RichTextBox.Text = _receiveContent.ToString();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isStop = true;
            LinkServer_Button.Enabled = true;
            if(_clientSockect.Connected)
            {
                _clientSockect.Close();
            }
            _clientSockect?.Dispose();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            Send_Button.Enabled = false;
        }
    }
}
