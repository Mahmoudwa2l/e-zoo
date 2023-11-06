using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using SocketClient;
//using System.Diagnostics.Metrics;

namespace SocketClient
{
    class ClientC
    {
        int byteCount;
        NetworkStream stream;
        byte[] sendData;
        TcpClient tcpClient;

        public bool connectToSocket(String host, int portNumber)
        {
            try
            {
                tcpClient = new TcpClient(host, portNumber);
                stream = tcpClient.GetStream();
                Console.WriteLine("Connection Made ! with " + host);
                return true;
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("Connection Failed: " + e);
                return false;
            }
        }

        public bool recieveMessage()
        {
            //try
            //{
            //    // Receive an integer from the peer.
            //    byte[] receiveBuffer = new byte[sizeof(int)];
            //    int bytesRead = stream.Read(receiveBuffer, 0, sizeof(int));

            //    if (bytesRead == sizeof(int))
            //    {
            //        int receivedInt = BitConverter.ToInt32(receiveBuffer, 0);
            //        Console.WriteLine("Received integer: " + receivedInt);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Received data is not a valid integer.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("An error occurred: " + ex.Message);
            //}

            try
            {
                // Receive some data from the peer.
                byte[] receiveBuffer = new byte[1024];
                int bytesReceived = stream.Read(receiveBuffer);
                Console.log(bytesReceived)
                string data = Encoding.UTF8.GetString(receiveBuffer.AsSpan(0, bytesReceived));
                string[] points = data.Split(",");
                for (int i = 0; i < points.Length; i++)
                {
                    Console.WriteLine(points[i]);
                }
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine("Connection not initialized : " + e);
                return false;
            }
        }


            public bool closeConnection()
        {
            stream.Close();
            tcpClient.Close();
            Console.WriteLine("Connection terminated ");
            return true;
        }

        static void Main(string[] args)
        {
            ClientC c = new ClientC();
            c.connectToSocket("localhost", 5000);
            c.recieveMessage();
            c.closeConnection();
        }
    }
}
