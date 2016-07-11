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
            serialPort.BaudRate = 2400; 
            serialPort.DataBits = 7; 
            serialPort.StopBits = StopBits.One;          
            serialPort.Parity = Parity.Odd; 
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500; 

            serialPort.Encoding = Encoding.GetEncoding("windows-1251");
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

        private void button3_Click(object sender, EventArgs e)
        {
            var encoding = Encoding.GetEncoding("KOI8-R");
            var bytes = encoding.GetBytes(textBox3.Text);
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = (byte)(bytes[i] & 0x7F);
            }
            var String = encoding.GetString(bytes);

            serialPort.WriteLine("AT\r\n");  
            System.Threading.Thread.Sleep(500);
            serialPort.Write("AT+CMGF=1\r\n"); 
            System.Threading.Thread.Sleep(500);
            System.Threading.Thread.Sleep(500);
            serialPort.Write("AT+CMGS=\"" + textBox2.Text + "\"" + "\r\n");
            System.Threading.Thread.Sleep(500);
            serialPort.Write(String + char.ConvertFromUtf32(26) + "\r\n");
            System.Threading.Thread.Sleep(500);
        }
    }
}
