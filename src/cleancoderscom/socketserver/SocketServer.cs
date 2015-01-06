using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//Conversion from Java to C# 
//Java Socket replaced with .NET TcpClient
//Java ServerSocket replaced with .NET TcpListener

namespace cleancoderscom.socketserver
{

    public class SocketServer
    {
        private readonly int port;
        private readonly SocketService service;
        private bool running;

        private TcpListener serverSocket;
        private ThreadCounter threadCounter;
       
        public SocketServer(int port, SocketService service)
        {
            this.port = port;
            this.service = service;
            var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            serverSocket = new TcpListener(ipAddress, port);
            serverSocket.Start();
            threadCounter = new ThreadCounter();
            
        }

        public virtual int Port
        {
            get
            {
                return port;
            }
        }

        public virtual SocketService Service
        {
            get
            {
                return service;
            }
        }

        public virtual void start()
        {
            running = true;
            
            ThreadPool.QueueUserWorkItem(
                (o) =>
                {
                    threadCounter.IncrementCount();

                    try
                    {
                        while (running)
                        {
                            var serviceSocket = serverSocket.AcceptTcpClient();
                            ThreadPool.QueueUserWorkItem(
                                (o1) =>
                                {
                                    threadCounter.IncrementCount();

                                    try
                                    {
                                        service.serve(serviceSocket);
                                    }
                                    finally
                                    {
                                        threadCounter.DecrementCount();
                                    }                                   
                                    
                                });
                        }

                    }
                    catch (Exception e)
                    {
                        if (running)
                            Console.WriteLine(e.StackTrace);
                    }
                    finally
                    {
                        threadCounter.DecrementCount();
                    }
                }
                );
            
        }

        public virtual bool Running
        {
            get
            {
                return running;
            }
        }

        public virtual void stop()
        {
            running = false;
            serverSocket.Stop();
            threadCounter.WaitAllToFinish();
        }

     
    }

}