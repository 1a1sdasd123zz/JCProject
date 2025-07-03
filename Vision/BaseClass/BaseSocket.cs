using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Vision.BaseClass
{
    public abstract class BaseSocket
    {
        public string _Name = "";

        public int _BufferSize = 8 * 1024;

        public Socket _Sock;
        public static IPEndPoint _Local;
        public System.Net.IPAddress _LocalIp;
        public int _LocalPort = 1000;
        public Socket _Sockc;


        public abstract bool Start();
        public abstract bool Close();

        //接收消息的方法
        public void ReceiveData(object obj)
        {
            using (var sock = obj as Socket)
            {
                try
                {
                   
                    while (sock.Connected)
                    {
                        
                        //定义字节，接收数据
                        byte[] buffer = new byte[_BufferSize];
                        int r = sock.Receive(buffer);
                        if (r == 0)
                        {
                            break;
                        }
                        string msg = Encoding.Default.GetString(buffer);
                        SocketMsg sm = new SocketMsg();
                        sm.sock = sock;
                        sm.msg = msg;
                        LogUtil.Log($">>>>IP:{_LocalIp},端口号:{_LocalPort},收到 {sock.RemoteEndPoint.ToString()} 数据为: {msg.TrimEnd('\0')}");
                    }
                }
                catch (SocketException ex)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
        }

        public static void SendData(SocketMsg Msg)
        {
            try
            {
                Socket sock = Msg.sock;
                string msg = Msg.msg;
                byte[] buffer = Encoding.Default.GetBytes(msg);
                sock.Send(buffer);
                LogUtil.Log("<<<<" + sock.RemoteEndPoint.ToString() + $"发送消息:{msg}");
            }
            catch { LogUtil.LogError("消息发送异常!"); }
        }

    }

    public class ServerSocket : BaseSocket
    {
        List<Socket> _ClientList = new List<Socket>();//客户端列表
        int _ClientCoun;
        int _MaxClientCount = 10;
        Thread _ThreadListen;
        //Thread _ThreadReceive;
        public ServerSocket(string name, string ip, string port)
        {
            _Name = name;
            _LocalIp = System.Net.IPAddress.Parse(ip);
            _LocalPort = Convert.ToInt32(port);
            _Local = new IPEndPoint(_LocalIp, _LocalPort);
            IniSocket();
        }
        ~ServerSocket()
        {
            Close();
        }

        private void IniSocket()
        {
            _Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _Sock.Bind(_Local);
        }
        public override bool Start()
        {
            try
            {
                Connet();
                LogUtil.Log("服务器(" + _Name + ")开启成功!");
                return true;
            }
            catch (SocketException ex)
            {
                LogUtil.LogError("服务器（" + _Name + "）开启异常，异常信息：" + ex.ToString());
                return false;
            }
        }

        public override bool Close()
        {
            {
                try
                {
                    if (_Sock != null && _Sock.Connected)
                    {
                        _Sock.Shutdown(SocketShutdown.Both);
                        ThreadClose();

                        _Sock.Close();
                        LogUtil.Log("服务器（" + _Name + "）" + "断开成功");
                        return true;
                    }
                    return false;
                }
                catch (SocketException ex)
                {
                    LogUtil.LogError("服务器（" + _Name + "）" + "断开异常，异常信息：" + ex.ToString());
                    return false;
                }
            }
        }

        private bool Connet()
        {
            try
            {
                _Sock.Listen(_MaxClientCount);
                //开启监听
                _ThreadListen = new Thread(Listen);
                _ThreadListen.IsBackground = true;
                _ThreadListen.Start();
                return true;
            }
            catch (SocketException ex)
            {
                LogUtil.LogError("服务器（" + _Name + "）开启异常，异常信息：" + ex.ToString());
                return false;
            }
        }


        private void Listen()
        {
            try
            {
                //因为要持续监听，所以用了while循环
                while (true)
                {
                    //阻塞线程，直到有客户端进来的时候才会往下执行
                    var socketAccept = _Sock.Accept();
                    //AddClientList(socketAccept);
                    _ClientCoun++;
                    LogUtil.Log(socketAccept.RemoteEndPoint + $"加入连接,共有{_ClientCoun}连接");
                    //接收消息
                    Thread threadReceive = new Thread(ReceiveData);
                    threadReceive.IsBackground = true;
                    threadReceive.Start(socketAccept);

                }
            }
            catch
            {
                throw;
            }
        }

        private void AddClientList(Socket sk)
        {
            Socket sock = _ClientList.Find(o => { return o.RemoteEndPoint == sk.RemoteEndPoint; });
            //如果不存在则添加,否则更新
            if (sock == null)
            {
                _ClientList.Add(sk);
            }
            else
            {
                _ClientList.Remove(sock);
                _ClientCoun--;
                _ClientList.Add(sk);
            }
        }
    

        private void ThreadClose()
        {
            if (_ThreadListen != null && _ThreadListen.ThreadState == ThreadState.Running)
                _ThreadListen.Abort();
        }
    }

    public class ClientSocket:BaseSocket
    {
        Thread _ThreadReceive;
        public ClientSocket(string name, string ip, string port)
        {
            _Name = name;
            _LocalIp = System.Net.IPAddress.Parse(ip);
            _LocalPort = Convert.ToInt32(port);
            _Local = new IPEndPoint(_LocalIp, _LocalPort);
            IniSocket();
        }

        private void IniSocket()
        {
            _Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public override bool Start()
        {
            try
            {
                _Sock.Connect(_Local);
                _ThreadReceive = new Thread(ReceiveData);
                _ThreadReceive.IsBackground = true;
                _ThreadReceive.Start();
                return true;
            }
            catch { return false; }
           
        }

        public override bool Close()
        {
            try
            {
                _Sock.Close();
                if (_ThreadReceive != null && _ThreadReceive.ThreadState == ThreadState.Running)
                {
                    _ThreadReceive.Abort();
                }
                return true;
            }
            catch { return false; }
        }
    }

    public class SocketMsg
    {
        public Socket sock { get; set; }
        public string msg { get; set; }
    }
}
