using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

using NUnit.Framework;
using cleancoderscom.socketserver;


//Conversion from Java to C# 
//Java Socket replaced with .NET TcpClient
//Java ServerSocket replaced with .NET TcpListener

//http://stackoverflow.com/questions/209281/c-sharp-equivalent-to-javas-wait-and-notify
//http://www.codeproject.com/Articles/28785/Thread-synchronization-Wait-and-Pulse-demystified
//foo.notify() => Monitor.Pulse(foo)
//foo.notifyAll() => Monitor.PulseAll(foo)
//foo.wait() =>  Monitor.Wait(foo)

namespace cleancoderscom.tests.socketserver
{

    public class SocketServerTest
    {
        private static ClosingSocketService service;
        private static SocketServer server;
        private static int port;

        static SocketServerTest()
        {
            port = 8042;  
        }

        //This method is not called by NUnit if there is no tests.
        [SetUp]
        public virtual void setUp()
        {
            port = 8042;
        }

        public abstract class TestSocketService : SocketService
        {
            public virtual void serve(TcpClient s)
            {
                try
                {
                    doService(s);
                    Notify();
                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }

            bool block = true;

            private void Notify()
            {
                lock (this)
                {
                    block = false;
                    Monitor.Pulse(this);
                }
            }

            public void Wait()
            {
                lock (this)
                {
                    while (block)
                    {
                        Monitor.Wait(this);
                    }
                    block = true;
                }

            }

            protected internal abstract void doService(TcpClient s);
        }

        #region WithClosingSocketService

        public class ClosingSocketService : TestSocketService
        {
            public int connections;
            protected internal override void doService(TcpClient s)
            {
                connections++;
            }
        }

        public class WithClosingSocketService
        {

            [SetUp]
            public virtual void setUp()
            {
                SocketServerTest.service = new ClosingSocketService();
                SocketServerTest.server = new SocketServer(SocketServerTest.port, SocketServerTest.service);
            }

            [TearDown]
            public virtual void tearDown()
            {
                SocketServerTest.server.stop();
            }

            [Test]
            public virtual void instantiate()
            {
                Assert.AreEqual(SocketServerTest.port, SocketServerTest.server.Port);
                Assert.AreEqual(service, SocketServerTest.server.Service);
            }

            [Test]
            public virtual void canStartAndStopServer()
            {
                SocketServerTest.server.start();
                Assert.IsTrue(SocketServerTest.server.Running);
                SocketServerTest.server.stop();
                Assert.IsFalse(SocketServerTest.server.Running);
            }

            [Test]
            public virtual void acceptsAnIncomingConnection()
            {
                SocketServerTest.server.start();
                var temp = new TcpClient("localhost", SocketServerTest.port);
                SocketServerTest.service.Wait();
                SocketServerTest.server.stop();

                Assert.AreEqual(1, SocketServerTest.service.connections);
            }

            [Test]
            public virtual void acceptsMultipleIncomingConnections()
            {
                SocketServerTest.server.start();
                var temp = new TcpClient("localhost", SocketServerTest.port);
                SocketServerTest.service.Wait();
                
                temp = new TcpClient("localhost", SocketServerTest.port);
                SocketServerTest.service.Wait();

                SocketServerTest.server.stop();

                Assert.AreEqual(2, SocketServerTest.service.connections);
            }
        } 
        #endregion

        #region WithReadingSocketService

        public class ReadingSocketService : TestSocketService
        {
            public string message;

            protected internal override void doService(TcpClient s)
            {
                Stream @is = s.GetStream();
                StreamReader br = new System.IO.StreamReader(@is);
                message = br.ReadLine();
            }
        }

        public class WithReadingSocketService
        {

            internal ReadingSocketService readingService;

            [SetUp]
            public virtual void setup()
            {
                readingService = new ReadingSocketService();
                SocketServerTest.server = new SocketServer(SocketServerTest.port, readingService);
            }

            [Test]
            public virtual void canSendAndReceiveData()
            {
                SocketServerTest.server.start();
                TcpClient s = new TcpClient("localhost", SocketServerTest.port);
                System.IO.Stream os = s.GetStream();
                var buffer = Encoding.UTF8.GetBytes("hello\n");
                os.Write(buffer, 0, buffer.Length);

                readingService.Wait();
                SocketServerTest.server.stop();

                Assert.AreEqual("hello", readingService.message);
            }
        } 
       
        #endregion

        #region WithEchoSocketService
        public class EchoSocketService : TestSocketService
        {
            protected internal override void doService(TcpClient s)
            {
            }
        }

        public class WithEchoSocketService
        {

            internal ReadingSocketService readingService;
            [SetUp]
            public virtual void setup()
            {
            }
            [Test]
            [Ignore("Not implemented at Episode 5")]
            public virtual void canSendAndReceiveData()
            {
            }
        } 
        #endregion
    }
}