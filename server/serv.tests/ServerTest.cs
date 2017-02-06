using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;

namespace serv.tests
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void StartConnect()
        {
            var server = new serv();           
            TcpListener listen = serv.StartConnection("192.168.0.13", 8080);
            Assert.IsInstanceOfType(listen, typeof(TcpListener));           

        }

        [TestMethod]
        public void AwaitConnect()
        {
            var server = new serv();
            TcpListener listen = new TcpListener(IPAddress.Parse("192.168.0.13"), 8080);
            listen.Start();
            Socket s = serv.AwaitClientConnection(listen);
            Assert.IsInstanceOfType(s, typeof(Socket));

        }

        [TestMethod]
        public void Data()
        {
            var server = new serv();
            TcpListener listener = serv.StartConnection("192.168.0.13", 8080);
            Socket s = serv.AwaitClientConnection(listener);
            bool flag = serv.GetData(s);
            Assert.IsInstanceOfType(flag, typeof(bool));
        }

        [TestMethod]
        public void acknowledge()
        {
            var server = new serv();
            TcpListener listener = serv.StartConnection("192.168.0.13", 8080);
            Socket s = serv.AwaitClientConnection(listener);           
            bool flag = serv.GetData(s);
            serv.Ack(s);
            Assert.IsTrue(s.Connected);
        }

        [TestMethod]
        public void end()
        {
            var server = new serv();
            TcpListener listener = serv.StartConnection("192.168.0.13", 8080);
            Socket s = serv.AwaitClientConnection(listener);
            bool flag = serv.GetData(s);
            serv.Ack(s);
            serv.CloseConnection(listener, s);
            Assert.IsFalse(s.Connected);
        }
    }
}
