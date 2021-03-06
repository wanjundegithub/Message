using java.lang;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exception = System.Exception;
using Thread = System.Threading.Thread;

namespace TCPServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private Socket _watchSocket = null;

        private delegate Task WatchDelegate();

        private bool _isStop = false;

        private Dictionary<string, Socket> _ipSocketDics = new Dictionary<string, Socket>();

        private Dictionary<string, Task> _ipTaskDics = new Dictionary<string, Task>();

        private delegate Task listenDelegate(Socket socket);

        private StringBuffer _receiveContent = new StringBuffer();

        private delegate Task SendDelegate(string text);

        private CancellationToken _listentCancellationToken;

        private CancellationTokenSource _listenCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServer_Button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ServerIP_TextBox.Text))
            {
                MessageBox.Show("ip为空");
                return;
            }
            if (string.IsNullOrEmpty(ServerPort_TextBox.Text))
            {
                MessageBox.Show("端口为空");
                return;
            }
            IPAddress iPAddress = IPAddress.Parse(ServerIP_TextBox.Text.Trim());
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, int.Parse(ServerPort_TextBox.Text.Trim()));
            _watchSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                 _watchSocket.Bind(iPEndPoint);
            }
            catch(SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            _watchSocket.Listen(10);
            Send_Button.Enabled = true;
            _listentCancellationToken = _listenCancellationTokenSource.Token;
            Task.Run(async()=>
            {
                await Listen();
            },_listentCancellationToken);
            ShowMessage("启动服务器成功\r\n");
            StartServer_Button.Enabled = false;
        }

        private async Task Listen()
        {
            if(IPCollection_listBox.InvokeRequired)
            {
                var d = new WatchDelegate(WatchTask);
                IPCollection_listBox.BeginInvoke(d);
            }
            else
            {
                await WatchTask();
            }
        }

        private  async Task WatchTask()
        {
            while(!_isStop)
            {
                //acceppt方法阻塞
                Socket acceptSockect =await _watchSocket.AcceptAsync();
                if(_ipSocketDics.ContainsKey(acceptSockect.RemoteEndPoint.ToString()))
                {
                    continue;
                }
                IPCollection_listBox.Items.Add(acceptSockect.RemoteEndPoint.ToString());
                _ipSocketDics.Add(acceptSockect.RemoteEndPoint.ToString(), acceptSockect);
                 Task task =  new Task(async() =>
                  {
                      await Receive(acceptSockect);
                  });
                task.Start();
                _ipTaskDics.Add(acceptSockect.RemoteEndPoint.ToString(), task);
            }
        }

        private async Task Receive(Socket connection)
        {
            if (Receive_RichTextBox.InvokeRequired)
            {
                var d = new listenDelegate(ReceiveMessage);
                Receive_RichTextBox.Invoke(d, connection);
                return;
            }
            else
            {
                await ReceiveMessage(connection);
            }
        }



        private async Task ReceiveMessage(Socket socket)
        {
            byte[] data = new byte[1024 * 1024 * 2];
            int length = -1;
            while(true)
            {
               if(_listentCancellationToken.IsCancellationRequested)
                {
                    return;
                }
                try
                {
                    if(socket.Connected)
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                length = socket.Receive(data);
                            }
                            catch (SocketException se)
                            {
                                ExceptionHandle("异常" + se.Message + "\r\n", socket);
                                return;
                            }
                        });
                    }
                    else
                    {
                        return;
                    }
                }
                catch(SocketException se)
                {                  
                    ExceptionHandle("异常" + se.Message + "\r\n", socket);
                    break;
                }
                catch(Exception e)
                {                 
                    ExceptionHandle("异常" + e.Message + "\r\n",socket);
                    break;
                }
                if(length!=0)
                {
                    if(data[0]==0)
                    {
                        string message = Encoding.UTF8.GetString(data,1,data.Length-1).Trim().Replace("\0",string.Empty);
                        ShowMessage(message);
                    }
                }
            }
        }

        private delegate void ShowDelegate(string message,Socket socket);

        private void ExceptionHandle(string message,Socket socket)
        {
           
            if (Receive_RichTextBox.InvokeRequired)
            {
                var d = new ShowDelegate(ExceptionShow);
                Receive_RichTextBox.Invoke(d, new object[] { message,socket });
            }
            else
            {
                ExceptionShow(message, socket);
            }  
        }

        private void ExceptionShow(string message,Socket socket)
        {
            ShowMessage(message);
            var ipKey = socket.RemoteEndPoint.ToString();
            _ipSocketDics.Remove(ipKey);
            _ipTaskDics.Remove(ipKey);
            IPCollection_listBox.Items.Remove(ipKey);            
        }

        private void ShowMessage(string message)
        {
            _receiveContent.append(message);
            Receive_RichTextBox.Text = _receiveContent.ToString();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Send_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Send_RichTextBox.Text))
            {
                MessageBox.Show("发送消息为空");
                return;
            }           
            string message = $"server:\r\n-->   {Send_RichTextBox.Text}\r\n";     
            await SendMessage(message);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        private  async Task SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                byte[] sendMessage = new byte[data.Length + 1];
                // 用来表示发送的是消息数据
                sendMessage[0] = 0;
                Buffer.BlockCopy(data, 0, sendMessage, 1, data.Length);
                if(IPCollection_listBox.SelectedIndex==-1)
                {
                    MessageBox.Show("未选择发送人");
                    return;
                }
                string selectItem = IPCollection_listBox.SelectedItem.ToString();
                Socket socket = _ipSocketDics[selectItem];
                await Task.Run(()=>
                {
                    socket.Send(sendMessage);
                });             
                ShowMessage(message);
            }
            catch (SocketException se)
            {
                ShowMessage(se.Message);
                return;
            }
            Send_RichTextBox.Text = string.Empty;
        }

        private void Server_Load(object sender, EventArgs e)
        {
            _receiveContent = new StringBuffer();
            Send_Button.Enabled = false;
        }

    }
}
