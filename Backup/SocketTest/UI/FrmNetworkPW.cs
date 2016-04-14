using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketTest.UI
{
    public partial class FrmNetworkPW : Form
    {
        


        public FrmNetworkPW()
        {
            InitializeComponent();
        }

        private void FrmNetworkPW_Load(object sender, EventArgs e)
        {
            tbxPw.Text = "1234";
            
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
