using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SocketTest
{
    /*
     * 用户数据: 
        目标: FF FF FE	
        源头: FC 38 F0		-FC:设备ID(公共地址) 38:网段ID(公共网段) F0:PC类型
        分页: 11		
        命令: 81 FE	
        长度: 36（4+n）
        数据: 00 00 00 00 00 00 -保留
              33 FF D2 05 42 53 33 34 31 66 21 43 -物理ID
              00 00 -设备位置	      
              D0 C2 D1 F4 B9 E2 B7 BF 00 00 
              00 00 00 00 00 00 00 00 00 00
              00 00 00 00 00 00 00 00 00 00  -30个字节为设备名称
              7E F3 A0 A8 -CRC校验  
     * 
     */

    public class NetworkData
    {

        //------定义全局对应--------
        public static readonly string DEVICE_ID = "DeviceID";//设备ID
        public static readonly string NETWORK_ID = "NetworkID";//网段ID
        public static readonly string STATE = "State";//连接状态
        public static readonly string DEVICE_NAME = "DeviceName";//设备名称
        public static readonly string MAC = "MAC";//物理ID
        public static readonly string CONNECTED = "已连接";//物理ID
        public static readonly string NOT_CONNECTED = "未连接";//物理ID

        public string DeviceId = "";
        public string NetworkId = "";
        public string State = "";
        public string DeviceName = "";
        public string MacAddress = "";



        /// <summary>
        /// 构造函数
        /// </summary>
        public NetworkData(UserUdpData userUdpData)
        {
            //this.TableName = "Network";
            //this.Columns.Add(DEVICE_ID, System.Type.GetType("System.String"));
            //this.Columns.Add(NETWORK_ID, System.Type.GetType("System.String"));
            //this.Columns.Add(STATE, System.Type.GetType("System.String"));
            //this.Columns.Add(DEVICE_NAME, System.Type.GetType("System.String"));
            //this.Columns.Add(MAC, System.Type.GetType("System.String"));

            DeviceId = Convert.ToInt16(userUdpData.Source[0]).ToString();
            NetworkId = Convert.ToInt16(userUdpData.Source[1]).ToString();
            State = NOT_CONNECTED;
            //-------MAC地址---------
            byte[] byteMac = new Byte[12];
            Buffer.BlockCopy(userUdpData.Data, 16, byteMac, 0, 12);
            MacAddress = ByteConvert.byteToHexStr(byteMac);
            //-------设备名称---------
            byte[] byteName = new Byte[30];
            Buffer.BlockCopy(userUdpData.Data, 20, byteName, 0, 30);
            DeviceName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');//.Replace("网关:","");

        }

        

    }


}
