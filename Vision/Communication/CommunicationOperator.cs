using Cognex.VisionPro.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class CommunicationOperator
    {
        public static CommCollection commCollection = new CommCollection();

        public static Dictionary<string, CogCommCard> cardDic;

        public static Dictionary<string, BTWTcpClient> clientDic;

        public static Dictionary<string, BTWTcpServer> serverDic;

        public static Dictionary<string, S7_SlaveStation> slaveDic;

        public static void EnumCards()
        {
            CC24_Comm.EnumCards();
            cardDic = CC24_Comm.D_cardList;
        }

        public static int OpenComm(CommConfigData mcommConfigData)
        {
            if (cardDic.ContainsKey(mcommConfigData.SerialNum))
            {
                if (!commCollection.ListKeys.Contains(mcommConfigData.SerialNum))
                {
                    new CC24_Comm(mcommConfigData.SerialNum).Start(mcommConfigData);
                }
                return 0;
            }
            if (mcommConfigData.SerialNum.Contains("Tcp-"))
            {
                if (mcommConfigData.RoleName == "Server")
                {
                    BTWTcpServer.CreateNewServer(mcommConfigData.LocalIp, Convert.ToInt32(mcommConfigData.LocalPort), 3, mcommConfigData.SerialNum);
                    BTWTcpServer.GetServerInstance(mcommConfigData.SerialNum).Start();
                }
                else
                {
                    BTWTcpClient.CreateNewClient(mcommConfigData.LocalIp, Convert.ToInt32(mcommConfigData.LocalPort), mcommConfigData.RemoteIp, Convert.ToInt32(mcommConfigData.RemotePort), mcommConfigData.SerialNum);
                    BTWTcpClient.GetClientInstance(mcommConfigData.SerialNum).Connect();
                }
                return 0;
            }
            if (mcommConfigData.SerialNum.Contains("S7-Slave-"))
            {
                S7_SlaveStation.CreateNewSlave(mcommConfigData.RemoteIp, Convert.ToInt32(mcommConfigData.RemotePort), (byte)mcommConfigData.Rack, (byte)mcommConfigData.Slot, mcommConfigData.ControlDBNum, mcommConfigData.StatusDBNum, mcommConfigData.SerialNum);
                S7_SlaveStation.GetSlaveInstance(mcommConfigData.SerialNum).Connect();
                return 0;
            }
            return -1;
        }

        public static IFlowState FindComm(CommConfigData mcommConfigData)
        {
            if (commCollection._commDic.ContainsKey(mcommConfigData.SerialNum))
            {
                return commCollection._commDic[mcommConfigData.SerialNum];
            }
            return null;
        }
    }
}
