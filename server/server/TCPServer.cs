using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace serv
{

    public class serv
    {
        public TcpListener listener { get; set; }
        public static void Main()
        {


            try
            {
                TcpListener listener = StartConnection("192.168.0.13", 8080);
                Socket s = AwaitClientConnection(listener);
                bool close_flag = false;
                while (!close_flag)
                {
                    close_flag = GetData(s);
                    Ack(s);
                }
                CloseConnection(listener, s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }


        public static TcpListener StartConnection(string ipaddress, int port)
        {
            // need to change this to your ip address
            IPAddress listenerAddress = IPAddress.Parse(ipaddress);
            /* Initializes the Listener */
            TcpListener listener = new TcpListener(listenerAddress, port);
            /* Start Listeneting at the specified port */
            listener.Start();
            Console.WriteLine("The server is running at port 8001...");
            Console.WriteLine("The local End point is  :" + listener.LocalEndpoint);
            return listener;
        }

        public static Socket AwaitClientConnection(TcpListener listener)
        {
            Console.WriteLine("Waiting for a connection.....");
            Socket s = listener.AcceptSocket();
            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
            return s;
        }

        public static bool GetData(Socket s)
        {
            byte[] data_buffer = new byte[100];
            int k = s.Receive(data_buffer);
            Console.WriteLine("Recieved...\n");
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(data_buffer[i]));
                tmp.Append(Convert.ToChar(data_buffer[i]));
            }
            string close_string = tmp.ToString();
            close_string.ToLower();
            if (close_string == "close")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Ack(Socket s)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("The string was recieved by the server."));
            Console.WriteLine("\nSent Acknowledgement");
        }

        public static void CloseConnection(TcpListener listener, Socket s)
        {
            s.Close();
            listener.Stop();
        }

    }
}