
using System;
using System.Collections.Generic;
using System.Text;


namespace SocketTest
{

    //
    // 摘要:UdpData.cs
    //     封装UDP协议实例
    public class UdpData
    {
        /*
         * 
         *
    	---------------------------------------------------------------------------
    	系统标识符：用于区分不同的系统，如：“AYLSON smarthome”  （占16字节<字符>）
    	系统识别码：用于区分在同一网络中同类型系统，目前固定为“0”     （占2字节）
    	---------------------------------------------------------------------------
    	包标识码：本包数据的包标识码，用于标识<发送>与<返回>是一对；    （占2字节）
    	包数据类：本包数据类型:发送数据,返回确认/数据；                 （占1字节）
    	包 属 性：本包是:(’B’广播)(’M’组播)(’U’单播)        （占1字节）
    	接收端口：发送此包数据设备的接收端口(如{D2,04}接收端口为1234) （占2字节）
    	---------------------------------------------------------------------------
    	用户协议：用户使用哪种自定义协议 或其它扩展功能                 （占4字节）
    	用户数据：用户自定义通讯协议数据；                              （0~n字节）
    	---------------------------------------------------------------------------
    	校 验 和：保留（本包数据校验和）       
 
         * 
        1．通信数据包格式：
        设备ID + 网段ID + 类型号+ 源设备ID + 源网段ID + 类型+ 页 + 控制字（2字节） + 长度+ 数据 + 4byte CRC
        长度 =  数据 + 4byte CRC
        页：用于指出当数据太长时被分割成多少次发送，高4位指出共有多少页，低4位指出当前的数据是第几次数据，当两者相等时则表示数据已发送完成。
        例：如果只需要发送一次数据则可完成，则页为0x11
        一次通信的最大为128个字节，如果超过则需要切割，切割后控制字后的第一个数据在每组中都需要发送。

        2．地址的分配：见SmartLight.H

        3．通信方式：CSMA/CD
        当有按键按下时自行发送数据，等待接收方返回数据，失败重发，最多重发三次。注意：如果数据有冲突，不算在重发三次的范围内。

         * 
         * 
         * */
        //-------基本协议----------
        public byte[] SysIdName;//----系统标识符----
        public byte[] SysIdCode;//----系统识别码----
        public byte[] PacketCode;//----包标识码----
        public byte[] PacketKind;//----包数据类----
        public byte[] PacketProperty;//----包属性----
        public byte[] ReceivePort;//----接收端口----
        public byte[] Protocol;//----用户协议----
        public byte[] ProtocolData;//----用户数据----
        public byte[] CheckCodeAdd;//----校验和----
        public byte[] AllData;//------所有数据内容--------
        public int Length;//---数据长度------



        //-------字段命名规则,用于包数据列表------
        public static readonly string XTBSF = "SysIdName";
        public static readonly string XTSBM = "SysIdCode";
        public static readonly string BBSM = "PacketCode";
        public static readonly string BSJL = "PacketKind";
        public static readonly string BSX = "PacketProperty";
        public static readonly string JSDK = "ReceivePort";
        public static readonly string YHXY = "Protocol";
        public static readonly string YHSJ = "ProtocolData";
        public static readonly string JYH = "CheckCodeAdd";
        public static readonly string SYSJ = "AllData";
        public static readonly string SJCD = "Length";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">接收UDP包</param>
        public UdpData(byte[] data)
        {
            //------基本协议--------
            SysIdName = new byte[16];
            SysIdCode = new byte[2];
            PacketCode = new byte[2];
            PacketKind = new byte[1];
            PacketProperty = new byte[1];
            ReceivePort = new byte[2];
            Protocol = new byte[4];
            CheckCodeAdd = new byte[1];
            Length = data.Length;
            int dataLength = Length - 16 - 2 - 2 - 1 - 1 - 2 - 4 - 1;
            ProtocolData = new byte[dataLength];
            AllData = new byte[Length];

            Buffer.BlockCopy(data, 0, SysIdName, 0, 16);
            Buffer.BlockCopy(data, 16, SysIdCode, 0, 2);
            Buffer.BlockCopy(data, 18, PacketCode, 0, 2);
            Buffer.BlockCopy(data, 20, PacketKind, 0, 1);
            Buffer.BlockCopy(data, 21, PacketProperty, 0, 1);
            Buffer.BlockCopy(data, 22, ReceivePort, 0, 2);
            Buffer.BlockCopy(data, 24, Protocol, 0, 4);
            Buffer.BlockCopy(data, 28, ProtocolData, 0, ProtocolData.Length);
            Buffer.BlockCopy(data, 28 + ProtocolData.Length, CheckCodeAdd, 0, 1);
            Buffer.BlockCopy(data, 0, AllData, 0, Length);
        }

 
        public string GetUdpInfo()
        {
            string info = "";
            info += "系统标识符:" + byteToHexStr(SysIdName) + "\n";
            info += "系统识别码:" + byteToHexStr(SysIdCode) + "\n";
            info += "包标识码:" + byteToHexStr(PacketCode) + "\n";
            info += "包数据类:" + byteToHexStr(PacketKind) + "\n";
            info += "包属性:" + byteToHexStr(PacketProperty) + "\n";
            info += "接收端口:" + byteToHexStr(ReceivePort) + "\n";
            info += "用户协议:" + byteToHexStr(Protocol) + "\n";
            info += "用户数据:" + byteToHexStr(ProtocolData) + "\n";
            info += "校验和:" + byteToHexStr(CheckCodeAdd) + "\n";

            return info;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        private string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += " " + bytes[i].ToString("X2");
                }
                returnStr = returnStr.Substring(1);
            }
            return returnStr;
        }

    }




}
