﻿
using System;
using System.Collections.Generic;
using System.Text;


namespace SocketTest
{

    //
    // 摘要:UserUdpData.cs
    //     封装用户协议实例
    public class UserUdpData
    {
        /*
         *
         1．通信数据包格式：
            设备ID + 网段ID + 类型号+ 源设备ID + 源网段ID + 类型+ 页 + 控制字（2字节） + 长度+ 数据 + 4byte CRC
            长度 =  数据 + 4byte CRC
            页：用于指出当数据太长时被分割成多少次发送，高4位指出共有多少页，低4位指出当前的数据是第几次数据，当两者相等时则表示数据已发送完成。
            例：如果只需要发送一次数据则可完成，则页为0x11
            一次通信的最大为128个字节，如果超过则需要切割，切割后控制字后的第一个数据在每组中都需要发送。
 
         *
            目  标	公共地址 + 公共网段 + RJ45/PC类型                    -----3个字节-------
            源  头	公共地址 + 公共网段 + RJ45/PC类型                    -----3个字节-------
            分  页	0x11                                                 -----1个字节-------
            命  令	CMD_PC_SEARCH                                        -----2个字节-------
            长  度	数据 + 4byte CRC                                     -----1个字节-------
            数  据	                                                     总字节数 - 3 - 3 - 1 - 2 - 1 - 4
            校  验	略                                                      -----4个字节-------
         * 
         * */

        //-------用户协议--------
        public byte[] Target;//----目标----
        public byte[] Source;//----源头----
        public byte[] Page;//------分页-----
        public byte[] Command;//---命令-----
        public byte[] DataLength;//数据长度---
        public byte[] Data;//------数据-----
        public byte[] CrcCode;//------CRC-----
        public int Length;//----总长度-----


        public UserUdpData(byte[] data)
        {
            //------基本协议--------
            Target = new byte[3];
            Source = new byte[3];
            Page = new byte[1];
            Command = new byte[2];
            DataLength = new byte[1];
            CrcCode = new byte[4];
            Length = data.Length;


            Buffer.BlockCopy(data, 0, Target, 0, 3);
            Buffer.BlockCopy(data, 3, Source, 0, 3);
            Buffer.BlockCopy(data, 6, Page, 0, 1);
            Buffer.BlockCopy(data, 7, Command, 0, 2);
            Buffer.BlockCopy(data, 9, DataLength, 0, 1);

            //---获取数据长度,根据协议减去4个字节的校验码长度----
            int dataLength = Convert.ToInt16(DataLength[0]) - 4;
            Data = new byte[dataLength];
            Buffer.BlockCopy(data, 10, Data, 0, dataLength);//-----数据-----
            Buffer.BlockCopy(data, 10 + dataLength, CrcCode, 0, 4);//----校验码---
        }

        public string GetUdpInfo()
        {
            string info = "";
            info += "目标:" + byteToHexStr(Target) + "\n";
            info += "源头:" + byteToHexStr(Source) + "\n";
            info += "分页:" + byteToHexStr(Page) + "\n";
            info += "命令:" + byteToHexStr(Command) + "\n";
            info += "长度:" + byteToHexStr(DataLength) + "\n";
            info += "数据:" + byteToHexStr(Data) + "\n";
            info += "CRC校验:" + byteToHexStr(CrcCode) + "\n";

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
