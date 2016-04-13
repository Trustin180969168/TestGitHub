using System;
using System.Collections.Generic;

using System.Text;
using System.Net;

namespace SocketTest
{

    //
    // 摘要:Command.cs
    //     用于保存所有设备指令
    public class SysConfig
    {

        public static int LOCAL_PORT = 9500;
        public static byte[] SYS_ID_NAME = ByteConvert.StrToToHexByte("41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65");
        public static string LOCAL_PORT_STR = "1C25";
        public static int REMOTE_PORT = 11211;
        /// <summary>
        /// 单例模式
        /// </summary>
        private SysConfig()
        {

        }
        private static readonly SysConfig instance = new SysConfig();
        public static SysConfig GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// 本地IP
        /// </summary>
        public static IPAddress LocalIP
        {
            get
            {
                ///获取本地的IP地址
                string AddressIP = string.Empty;
                foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        
                        AddressIP = _IPAddress.ToString();
                        if(AddressIP.Contains("172")) continue;
                        else 
                            break;
                    }
                }
                return IPAddress.Parse(AddressIP);
            }
        }


    }







}
