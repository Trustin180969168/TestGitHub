﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Threading;



namespace SocketTest
{

    public class MySocket
    {
        private Socket mySocket;//Socket对象
        private static Object obj = new Object();//加锁对象


        /// <summary>
        /// 单例模式
        /// </summary>
        private MySocket()
        {
            IPEndPoint ipLocalPoint = new IPEndPoint(SysConfig.LocalIP, SysConfig.LOCAL_PORT);
            //定义网络类型，数据连接类型和网络协议UDP  
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            mySocket.Bind(ipLocalPoint);//绑定网络地址  
        }
        private static readonly MySocket instance = new MySocket();
        public static MySocket GetInstance()
        {
            return instance;
        }

        
        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <returns>UdpData</returns>
        public UdpData SendData(byte[] data, string receiveIP, int receivePort)
        {
            lock (obj)
            {
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);

                mySocket.SendTo(data, data.Length, SocketFlags.None, remotePoint);
                return ReceiveData(remotePoint);
            }
        }


        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包列表
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <param name="receivePort">接收次数</param>
        /// <returns>List<UdpData></returns>
        public List<UdpData> SendData(byte[] data, string receiveIP, int receivePort, int receiveCount)
        {       
            IPAddress ip = getValidIP(receiveIP);
            IPEndPoint ipep = new IPEndPoint(ip, receivePort);
            EndPoint remotePoint = (EndPoint)(ipep);

            mySocket.SendTo(data, data.Length, SocketFlags.None, remotePoint);

            List<UdpData> list = new List<UdpData>();
            while (receiveCount-- > 0)
            {
                UdpData udpData = ReceiveData(remotePoint);
                if (data != null) list.Add(udpData);
            }
            return list;        
        }    

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="remotePoint">接收端</param>
        public UdpData ReceiveData(EndPoint remotePoint)
        {
            byte[] data = new byte[128];
            //跨线程调用控件  
            //接收UDP数据报，引用参数remotePoint获得源地址  
            int rlen = mySocket.ReceiveFrom(data, ref remotePoint);
            if (rlen > 128) return null;//无效包数据
            if (rlen == 0) return null;//无效包数据
            Array.Resize(ref data, rlen);//重新设定长度
            UdpData udpData = new UdpData(data);//UDP数据对象        
            return udpData;
        }


        /// <summary>
        /// 关闭Socket
        /// </summary>
        public void Close()
        {
            mySocket.Close();
        }


        /// <summary>
        /// 测试IP是否有效
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private IPAddress getValidIP(string ip)
        {
            IPAddress lip = null;

            try
            {
                //是否为空 
                if (!IPAddress.TryParse(ip, out lip))
                {
                    throw new ArgumentException(
                    "IP无效，不能启动DUP");
                }
            }
            catch (Exception e)
            {
                //ArgumentException,  
                //FormatException,  
                //OverflowException 
                Console.WriteLine("无效的IP：" + e.ToString());
                return null;
            }
            return lip;
        }
    }
}
