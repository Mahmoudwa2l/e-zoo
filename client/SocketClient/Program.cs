using System;
using System.Windows.Forms;
using System.Collection.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Socket;
using SocketClient;

namespace SocketClient
{


class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create and show the form
            Application.Run(new Form1());
        }
    }
    class ClientC
    {
        int byteCount;
        NetworkStream stream;
        byte[] sendData;
        TcpClient tcpClient;

        public bool connectToSocket(string host , int portNumber)
        {
            try
            {
                tcpClient = new TcpClient(host , portNumber);
                stream = tcpClient.GetStream();
                console.WriteLine("Connecting Made ! with " + host );
                return true;
            }
            catch
            {
                console.WriteLine("connection Failed : "+ e);
                return false;
            }
        }

    }

    class MyForm : Form
    {
        public MyForm()
        {
            // Add controls or customize your form here if needed
            Text = "Hello, World!";
            MessageBox.Show("Hello, World!");
        }
    }
}