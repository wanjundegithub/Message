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
            Send_Button.Enabled = false;
        }


        private StringBuffer _receiveContent =new StringBuffer();

        private Socket _clientSockect;

        private bool _isSussessConnected = false;

        private delegate  Task ReceiveDelegate();

        private CancellationToken _listentCancellationToken;

        private CancellationTokenSource _listenCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LinkServer_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ClientIP_TextBox.Text))
            {
                MessageBox.Show("客户端IP为空");
                return;
            }
            if (string.IsNullOrEmpty(ClientPort_TextBox.Text))
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
                await _clientSockect.ConnectAsync(iPEndPoint);
            }
            catch (SocketException se)
            {
                ShowMessage("  " + se.Message + "\r\n");
                Receive_RichTextBox.Text = _receiveContent.ToString();
                return;
            }
            _isSussessConnected = true;
            LinkServer_Button.Enabled = false;
            Send_Button.Enabled = true;
            _listentCancellationToken = _listenCancellationTokenSource.Token;
            await Task.Run(async() =>
            {
               await Receive();
            }, _listentCancellationToken);
            ShowMessage("连接服务器成功" + "\r\n");
        }

        private async Task Receive()
        {
            if(Receive_RichTextBox.InvokeRequired)
            {
                var d = new ReceiveDelegate(ReceiveMessage);
                Receive_RichTextBox.BeginInvoke(d);
            }
            else
            {
                await ReceiveMessage();
            }
        }

        private async Task ReceiveMessage()
        {          
            while (true)
            {
                int length = -1;
                byte[] data = new byte[1024 * 1024 * 2];
                if (_listentCancellationToken.IsCancellationRequested)
                {
                    return;
                }
                try
                {
                    if (_clientSockect.Connected)
                    {
                        await Task.Run(() =>
                        {
                            if (!_clientSockect.Connected)
                            {
                                return;
                            }
                            else
                            {
                                try
                                {
                                    length = _clientSockect.Receive(data);
                                }
                                catch(SocketException e)
                                {
                                    ExceptionHandle("异常" + e.Message + "\r\n");
                                }
                            }
                        });
                    }
                    else
                    {
                        return;
                    }
                }
                catch(SocketException e)
                {
                    ExceptionHandle("异常" + e.Message + "\r\n");
                }
                catch(ArgumentException e)
                {
                    ExceptionHandle("异常" + e.Message + "\r\n");
                }
                catch(System.Exception e)
                {
                    ExceptionHandle("异常" + e.Message + "\r\n");
                }
                if (length != -1)
                {
                    if (data[0] == 0)
                    {
                        string message = Encoding.UTF8.GetString(data, 1, data.Length - 1).Trim().Replace("\0", string.Empty);
                        ShowMessage(message);
                    }
                }

            }
        }

        private delegate void ShowDelegate(string message);

        private void ExceptionHandle(string message)
        {
            if (Receive_RichTextBox.InvokeRequired)
            {
                var d = new ShowDelegate(ExceptionShow);
                Receive_RichTextBox.Invoke(d, new object[] { message });
            }
            else
            {
                ExceptionShow(message);
            }
        }

        private void ExceptionShow(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Send_Button_Click(object sender, EventArgs e)
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
            await SendMessage(message);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        private async Task SendMessage(string message)
        {       
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                byte[] sendMessage = new byte[data.Length + 1];
                // 用来表示发送的是消息数据
                sendMessage[0] = 0;
                Buffer.BlockCopy(data, 0, sendMessage, 1, data.Length);
                await Task.Run(()=>
                {
                    _clientSockect.Send(sendMessage);
                });
                ShowMessage(message);
            }
            catch(SocketException se)
            {
                ShowMessage("发送失败:"+se.Message);
            }
            finally
            {
                Send_RichTextBox.Text = string.Empty;
            }
        }
      
        private void StopListenTask()
        {
            _listenCancellationTokenSource.Cancel();
        }

        private void ShowMessage(string message)
        {
            _receiveContent.append(message);
            Receive_RichTextBox.Text = _receiveContent.ToString();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListenTask();
        }
    }
}
