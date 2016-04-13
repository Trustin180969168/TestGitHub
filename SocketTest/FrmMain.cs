using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Timers;

namespace SocketTest
{
    public partial class FrmMain : Form
    {
        private SysCtrl sysCtrl;
        MySocket socket;
        System.Timers.Timer timerRefrashNetwork = new System.Timers.Timer();//----1秒刷新一次网络----

        public FrmMain()
        {
            InitializeComponent();
            sysCtrl = new SysCtrl();

            //-------初始化列表字段名-------
            networkDeviceID.FieldName = NetworkData.DEVICE_ID;
            networkDeviceName.FieldName = NetworkData.DEVICE_NAME;
            networkID.FieldName = NetworkData.NETWORK_ID;
            networkDeviceMac.FieldName = NetworkData.MAC;
            networkState.FieldName = NetworkData.STATE;
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSocketClientTest_Load(object sender, EventArgs e)
        {
            //sysCtrl.Init();//初始化配置
            socket = MySocket.GetInstance();
            gvNetwork.BestFitColumns();

            timerRefrashNetwork = new System.Timers.Timer();
            timerRefrashNetwork.Interval = 1000;
            timerRefrashNetwork.Elapsed += new System.Timers.ElapsedEventHandler(timerRefrashNetwork_Elapsed);
        }

        /// <summary>
        /// 搜索网络
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSend_Click(object sender, EventArgs e)
        {
            timerRefrashNetwork.Stop();
            gcNetwork.DataSource = NetworkCtrl.SearchNetworks();
            timerRefrashNetwork.Start();
        }

        /// <summary>
        /// 刷新网络
        /// </summary>
        private void timerRefrashNetwork_Elapsed(object sender, ElapsedEventArgs e)
        {
            NetworkCtrl.RefreshNetwork();
        }
      
        /// <summary>
        /// 窗体关闭前退出socket
        /// </summary>
        private void FrmSocketClientTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.Close();
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        private void btConnectnetwork_Click(object sender, EventArgs e)
        {





        }



 



    }
}
