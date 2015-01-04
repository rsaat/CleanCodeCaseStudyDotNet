using System;
using System.Net;
using System.Net.Sockets;
using cleancoderscom.socketserver;
using NUnit.Framework;

namespace cleancoderscom.tests.socketserver
{

    public class SocketServerTest
    {
        private FakeSocketService service;
        private SocketServer server;
        private int port;

        [SetUp]
        public virtual void setUp()
        {
            service = new FakeSocketService();
            port = 8042;
            server = new SocketServer(port, service);
        }

        [TearDown]
        public virtual void tearDown()
        {
            server.stop();
        }

        [Test]
        public virtual void instantiate()
        {
            Assert.AreEqual(port, server.Port);
            Assert.AreEqual(service, server.Service);
        }

        [Test]
        public virtual void canStartAndStopServer()
        {
            server.start();
            Assert.IsTrue(server.Running);
            server.stop();
            Assert.IsFalse(server.Running);
        }

        [Test]
        public virtual void acceptsAnIncomingConnection()
        {
            server.start();
            var t = new TcpClient("localhost", port);
            server.stop();

            Assert.AreEqual(1, service.connections);
        }


        public class FakeSocketService : SocketService
        {
            public int connections;

            public virtual void serve(TcpClient s)
            {
                connections++;
                try
                {
                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
        }
    }

}