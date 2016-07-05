using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsSenderForm
{
    public partial class MainForm : Form
    {
        SerialPort serialPort = null;

        public MainForm()
        {
            InitializeComponent();
        }
              
        private void button1_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
           serialPort = new SerialPort(textBox1.Text);
           serialPort.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Dispose();
                serialPort = null;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            button2_Click(sender, e);
        }
    }
}
