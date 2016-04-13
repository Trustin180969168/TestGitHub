using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;


namespace SocketTest
{



    public class NetworkCtrl
    {

        private static DataTable dtNetwork = new DataTable("Network");//-----网络列表-----
        private static List<NetworkData> listConnectedNetworks = new List<NetworkData>();//-----登记已经连接的网络-----   

        /// <summary>
        /// 搜索网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public static DataTable SearchNetworks()
        {
            //----初始化表-------
            if (dtNetwork.Columns.Count == 0)
            {
                dtNetwork.Columns.Add(NetworkData.DEVICE_ID, System.Type.GetType("System.String"));
                dtNetwork.Columns.Add(NetworkData.NETWORK_ID, System.Type.GetType("System.String"));
                dtNetwork.Columns.Add(NetworkData.STATE, System.Type.GetType("System.String"));
                dtNetwork.Columns.Add(NetworkData.DEVICE_NAME, System.Type.GetType("System.String"));
                dtNetwork.Columns.Add(NetworkData.MAC, System.Type.GetType("System.String"));
            }
            dtNetwork.Clear(); dtNetwork.AcceptChanges();
            byte[] data = ByteConvert.StrToToHexByte(NetworkConfig.SeachNetworkStr);
            List<UdpData> listData =  MySocket.GetInstance().SendData(data, "255.255.255.255", SysConfig.REMOTE_PORT, 10);
            if (listData.Count == 0) return null;

            foreach (UdpData udp in listData)
            {
                UserUdpData userData = new UserUdpData(udp.ProtocolData);//----用户协议---
                NetworkData network = new NetworkData(userData);//----网络对象-----
                //------修改已经连接网络的状态----
                foreach (NetworkData connectNetwork in listConnectedNetworks)
                {
                    if (network.MacAddress == connectNetwork.MacAddress)
                        network.State = NetworkData.CONNECTED;
                }                
                //------添加到数据表----------
                dtNetwork.Rows.Add(new object[] { network.DeviceId, network.NetworkId, network.State, 
                    network.DeviceName, network.MacAddress });
            }
            return dtNetwork;

        }

        /// <summary>
        /// 刷新网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public static void RefreshNetwork()
        {
            foreach (NetworkData network in listConnectedNetworks)
            {
                //--------执行刷新-------


            }


        }

        
        /// <summary>
        /// 连接网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public static bool ConnectNetworks()
        {
            string cmdSeachNetworkStr = string.Format("41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 " +
                    "00 00 41 13 01 42{0}01 00 00 00 FF FF F0 FF FF FE 11 01 FE 0C FF FF FF FF FF FF FF FF 8F 90 BE 84 D9",
                    SysConfig.LOCAL_PORT_STR);

            return false;

        }

    }
}
