using System;
using System.Collections.Generic;
using System.Text;

namespace SocketTest
{
    public class NetworkConfig
    {

        //---------搜索网络------
        public static readonly string SeachNetworkStr = string.Format("41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 " +
                    "00 00 41 13 01 42{0}01 00 00 00 FF FF F0 FF FF FE 11 01 FE 0C FF FF FF FF FF FF FF FF 8F 90 BE 84 D9",
                    SysConfig.LOCAL_PORT_STR);
        //---------连接网络------
        public static readonly string ConnectNetworkStr = string.Format("41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 " +
            "00 00 41 13 01 42{0}01 00 00 00 FF FF F0 FF FF FE 11 01 FE 0C FF FF FF FF FF FF FF FF 8F 90 BE 84 D9",
            SysConfig.LOCAL_PORT_STR);        

    }

}
