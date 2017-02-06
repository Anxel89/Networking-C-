using System;
using System.IO;
using System.Text;
using System.Net.Sockets;

public class Pair
{
    Stream st;
    bool flag = false;

    public Stream St
    {
        get { return st; }
        set { st = value; }
    }

    public bool Flag
    {
        get
        {
            return flag;
        }
        set
        {
            flag = value;
        }
    }
    public Pair()
    {

    }
    public Pair(bool flag, Stream st)
    {
        this.flag = flag;
        this.st = st;
    }
}
public class clnt
{
    public static void Main()
    {

        try
        {
            TcpClient client = ConnectToServer("192.168.0.13", 8080);            
            bool close_flag = false;
            while (!close_flag)
            {
                Pair data = new Pair();
                data  = SendData(client);
                close_flag = data.Flag;
                Stream st = data.St;
                ReceiveData(st);
            }
            CloseConnection(client);            
        }

        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
        }
    }

    public static TcpClient ConnectToServer( string address, int port)
    {
        TcpClient client = new TcpClient();
        Console.WriteLine("Connecting.....");
        client.Connect(address,port); 
        Console.WriteLine("Connected");
        return client;
    }

    public static Pair SendData(TcpClient client)
    {
        Console.Write("Enter the string to be transmitted : ");
        String str = Console.ReadLine();
        Stream stm = client.GetStream();
        ASCIIEncoding asen = new ASCIIEncoding();
        byte[] data_to_send = asen.GetBytes(str);
        Console.WriteLine("Transmitting.....");
        stm.Write(data_to_send, 0, data_to_send.Length);
        str.ToLower();
        if(str == "close")
        {
            bool flag = true;
            Pair result = new Pair(flag, stm);
            return result;
            
        }
        else
        {
            bool flag = false;
            Pair result = new Pair(flag, stm);
            return result;
        }
       
    }

    public static void ReceiveData(Stream st)
    {
        byte[] data_stream = new byte[100];
        int k = st.Read(data_stream, 0, 100);
        for (int i = 0; i < k; i++)
            Console.Write(Convert.ToChar(data_stream[i]));
    }

    public static void CloseConnection(TcpClient client)
    {
        client.Close();
    }
}