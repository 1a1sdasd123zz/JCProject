using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Vision.BaseClass;

namespace Vision.Hardware.Frame_Grabber
{
    public class CameraSerialPort
    {
        private SerialPort m_port = new SerialPort();

        private bool isOpened;

        private object m_locker = new object();

        private int _readTimeout = 1000;

        private int _writeTimeout = 1000;

        private string _portName = string.Empty;

        private int _baudRate = 9600;

        private Stopwatch m_watch = new Stopwatch();

        private StringBuilder receiveBuffer = new StringBuilder();

        private bool isCompleted = false;

        public string PortName => _portName;

        public bool IsOpen()
        {
            return isOpened;
        }

        public CameraSerialPort(string portName, int baudRate)
        {
            _portName = portName;
            _baudRate = baudRate;
        }

        public CameraSerialPort(string portName, int baudRate, int readTimeout, int writeTimeout)
            : this(portName, baudRate)
        {
            _readTimeout = readTimeout;
            _writeTimeout = writeTimeout;
        }

        public bool OpenPort()
        {
            if (IsOpen())
            {
                return true;
            }
            isOpened = false;
            m_port.PortName = _portName;
            m_port.BaudRate = _baudRate;
            m_port.StopBits = StopBits.One;
            m_port.DataBits = 8;
            m_port.Parity = Parity.None;
            m_port.ReadTimeout = _readTimeout;
            m_port.WriteTimeout = _writeTimeout;
            Monitor.Enter(m_locker);
            try
            {
                m_port.Open();
                isOpened = true;
            }
            catch (Exception ex)
            {
                isOpened = false;
                LogUtil.LogError("串口(" + _portName + ")开启失败，异常信息：" + ex.Message);
            }
            Monitor.Exit(m_locker);
            return isOpened;
        }

        public bool ClosePort()
        {
            if (!IsOpen())
            {
                return true;
            }
            Monitor.Enter(m_locker);
            try
            {
                m_port.Close();
                isOpened = false;
            }
            catch (Exception ex)
            {
                isOpened = true;
                LogUtil.LogError("串口(" + _portName + ")关闭失败，异常信息：" + ex.Message);
            }
            Monitor.Exit(m_locker);
            return isOpened;
        }

        public string WriteDataToPortWithResponse(string strData, int nTimeout)
        {
            isCompleted = false;
            receiveBuffer.Clear();
            string strReturn = string.Empty;
            if (WriteDataToPort(strData))
            {
                strReturn = ReadDataFromPort(nTimeout);
            }
            return strReturn;
        }

        public bool WriteDataToPort(string strData)
        {
            bool bReturn = false;
            strData += "\r";
            Monitor.Enter(m_locker);
            try
            {
                m_port.Write(strData);
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            Monitor.Exit(m_locker);
            return bReturn;
        }

        public string ReadDataFromPort(int nTimeout)
        {
            string strReadData = "";
            m_watch.Restart();
            Monitor.Enter(m_locker);
            while (m_watch.Elapsed.TotalMilliseconds <= (double)nTimeout && IsOpen())
            {
                strReadData += m_port.ReadExisting();
            }
            Monitor.Exit(m_locker);
            m_watch.Stop();
            return strReadData;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            receiveBuffer.Append("");
            do
            {
                int count = m_port.BytesToRead;
                if (count <= 0)
                {
                    break;
                }
                byte[] readBuffer = new byte[count];
                m_port.Read(readBuffer, 0, count);
                receiveBuffer.Append(Encoding.ASCII.GetString(readBuffer));
            }
            while (m_port.BytesToRead > 0);
            isCompleted = true;
        }
    }
}
