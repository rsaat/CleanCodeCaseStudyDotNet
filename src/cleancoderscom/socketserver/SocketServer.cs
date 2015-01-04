using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace cleancoderscom.socketserver
{

	public class SocketServer
	{
	  private readonly int port;
	  private readonly SocketService service;
	  private bool running;

      /// Java ServerSocket replaced with .NET TcpListener  
      private TcpListener serverSocket;
	
	 public SocketServer(int port, SocketService service)
	 {
		this.port = port;
		this.service = service;
        var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
        serverSocket = new TcpListener(ipAddress,port);
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
        //call run method RunnableAnonymousInnerClassHelper in another thread 
        //It is not exact replacemnt of Java Executors.newFixedThreadPool(4);  
	    var runableHelpper = new RunnableAnonymousInnerClassHelper(this);
	    ThreadPool.QueueUserWorkItem(runableHelpper.ThreadPoolCallback);  
		running = true;
	  }

	  private class RunnableAnonymousInnerClassHelper 
	  {
		  private readonly SocketServer outerInstance;

		  public RunnableAnonymousInnerClassHelper(SocketServer outerInstance)
		  {
			  this.outerInstance = outerInstance;
		  }

	      public void ThreadPoolCallback(Object threadContext)
	      {
              run();
	      }

	      public virtual void run()
		  {
			try
			{
              outerInstance.serverSocket.Start();
			  var serviceSocket = outerInstance.serverSocket.AcceptTcpClient();
			  outerInstance.service.serve(serviceSocket);
			}
            catch (Exception e)
			{
			  if (outerInstance.running)
			  {
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			  }
			}
		  }
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
		//executor.awaitTermination(500, TimeUnit.MILLISECONDS);
        System.Threading.Thread.Sleep(500);
		serverSocket.Stop();
		running = false;
	  }
	}

}