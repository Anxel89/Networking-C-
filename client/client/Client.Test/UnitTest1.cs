using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace Client.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Connect()
        {
            var client = new clnt();
            TcpClient c = clnt.ConnectToServer("192.168.0.13", 8080);
            Assert.IsTrue(c.Connected);
            
        }

        [TestMethod]
        public void SendData()
        {
            var client = new clnt();
            TcpClient c = clnt.ConnectToServer("192.168.0.13", 8080);
            Pair data = new Pair();
            string send = "hello";
            // used to test primitive version of method before readline was introduced.
            //data = clnt.SendData(c, send);         
            Assert.IsTrue(data.St.ToString() ==  "The string was recieved by the server.");
        }

        [TestMethod]
        public void GetData()
        {
            var client = new clnt();
            TcpClient c = clnt.ConnectToServer("192.168.0.13", 8080);
            Pair data = new Pair();
            string send = "hello";
            // used to test primitive version of method before readline was introduced.
            //data = clnt.SendData(c, send);
            //clnt.ReceiveData(data.St);
            Assert.IsTrue(c.Connected);    
        }

        [TestMethod]
        public void Close()
        {
            var client = new clnt();
            TcpClient c = clnt.ConnectToServer("192.168.0.13", 8080);
            Pair data = new Pair();
            string send = "hello";
            // used to test primitive version of method before readline was introduced.
            //data = clnt.SendData(c, send);
            //clnt.ReceiveData(data.St);
            //clnt.CloseConnection(c);
            Assert.IsTrue(!c.Connected);
        }
    }


}
