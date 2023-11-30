using System;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class ClientC
    {
        NetworkStream? stream;
        TcpClient? tcpClient;

        public bool ConnectToSocket(string host, int portNumber)
        {
            try
            {
                tcpClient = new TcpClient(host, portNumber);
                stream = tcpClient.GetStream();
                Console.WriteLine("Connection Made! with " + host);
                return true;
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("Connection Failed: " + e);
                return false;
            }
        }

        public bool ReceiveMessage()
        {
            try
            {
                if (stream != null)
                {
                    byte[] receiveBuffer = new byte[1024];
                    int bytesReceived = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    if (bytesReceived > 0)
                    {
                        string data = Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
                        string[] points = data.Split(",");
                        for (int i = 0; i < points.Length; i++)
                        {
                            Console.WriteLine(points[i]);
                        }
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine("Connection not initialized.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error receiving message: " + e);
                return false;
            }
        }

        public bool CloseConnection()
        {
            if (stream != null)
            {
                stream.Close();
            }

            if (tcpClient != null)
            {
                tcpClient.Close();
            }

            Console.WriteLine("Connection terminated.");
            return true;
        }

        static void Main(string[] args)
        {
            ClientC c = new ClientC();
            if (c.ConnectToSocket("localhost", 5000))
            {
                c.ReceiveMessage();
                c.CloseConnection();
            }
        }
    }
}
