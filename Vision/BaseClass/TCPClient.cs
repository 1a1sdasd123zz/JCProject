using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Vision.BaseClass
{   
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public delegate void DelConnected(bool isConnected);
    public delegate void SocketMessage(string str);         //定义一个有一个string参数无返回值委托
    public class TCPClient //网口客户端类
        (TcpInfo tcpInfo)
    {   
        public SocketMessage m_SocketMsgDelegate = null;       //定义一个委托变量===》有一个string参数无返回值的方法（Send()）
        public Socket m_Handler = null;                         //定义一个收发数据的客户端Socket
        private bool _IsConnected = false;
        public event DelConnected Connected;    //通讯是否连接
        public TcpInfo mTcpInfo = tcpInfo;

        //private Thread _ThreadTcpHeartBeat;
        public bool IsConnected
        {
            get
            {
                return _IsConnected;
            }
        }


        private void Connect(TcpInfo tcpInfo)
        {
            //端口及IP  
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(tcpInfo.IP), Convert.ToInt32(tcpInfo.Port));
            //创建套接字  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //开始异步连接服务器  
            client.BeginConnect(ipe, new AsyncCallback(ConnectCallback), client);
        }

        public void Connect()
        {
            //端口及IP  
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(mTcpInfo.IP), Convert.ToInt32(mTcpInfo.Port));
            //创建套接字  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //开始异步连接服务器  
            client.BeginConnect(ipe, new AsyncCallback(ConnectCallback), client);
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Thread.Sleep(100);
                m_Handler = (Socket)ar.AsyncState;
                m_Handler.EndConnect(ar);

                if(m_Handler.Connected)
                {
                    _IsConnected = true;
                    if (Connected != null)
                    {
                        Connected(_IsConnected);
                    }
                    LogUtil.Log($"连接服务器:{mTcpInfo.IP}成功");
                }
                else
                {
                    _IsConnected = false;
                    LogUtil.Log($"连接服务器:{mTcpInfo.IP}失败！");
                }
                //创建状态对象    
                StateObject state = new StateObject();
                state.workSocket = m_Handler;

                //开启数据回调
                m_Handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                
            }
            catch (Exception ex)
            {
                if (Connected != null)
                {
                    Connected(false);
                }
                _IsConnected = false;
                LogUtil.LogError($"通讯连接失败 {ex}");
            }
        }

        private void ReceiveCallBack(IAsyncResult iar)
        {
            try
            {
                String content = String.Empty;
                StateObject state = (StateObject)iar.AsyncState;
                Socket handler = state.workSocket;

                //读取数据   
                int bytesRead =0;
                bytesRead= handler.EndReceive(iar);
                if (bytesRead > 0)
                {
                    //state.buffer = new byte[256];
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    content = state.sb.ToString();


                    if (m_SocketMsgDelegate != null)
                    {
                        m_SocketMsgDelegate(content);
                    }

                    state.sb.Clear();
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                }
                else
                {
                    if (Connected != null)
                    {
                        Connected(false);
                    }
                    _IsConnected = false;
                }
            }
            catch (Exception ex)
            {
                if (Connected != null)
                {
                    Connected(false);
                }
                _IsConnected = false;
                LogUtil.LogError($"通讯连接失败 {ex.ToString()}");
            }

        }

        public bool Send(String data)
        {
            try
            {
                byte[] byteData = new byte[4];
                byteData = Encoding.ASCII.GetBytes(data);
                m_Handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), m_Handler);
                return true;
            }
            catch { return false; }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = 0;
                bytesSent =handler.EndSend(ar);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SendHeartbeat()
        {
            while (true)
            {
                string str = "HBT\n";
                if (!this.Send(str))
                {
                    if (Connected != null)
                        Connected(false);
                    this.Connect(mTcpInfo);
                    LogUtil.LogError("与服务器连接断开，正在尝试重新连接!");
                }
                else
                {
                    if (Connected != null)
                        Connected(true);
                }
                Thread.Sleep(3000);
            }
        }

        public void CloseSocket()
        {
            m_Handler.Close();//关闭释放资源
        }
    }
}
