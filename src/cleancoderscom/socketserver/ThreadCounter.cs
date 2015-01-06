using System;
using System.Threading;

namespace cleancoderscom.socketserver
{
    /// <summary>
    ///    Implement a counter of the number of threads running. 
    ///    This class is created to emulate Java awaitTermination  for .Net ThreadPool
    /// </summary>
    public class ThreadCounter
    {
        private CountdownEvent countDown=null;


        private  void InititializeCount()
        {
            countDown = new CountdownEvent(1);
        }

        public void IncrementCount()
        {
            if (countDown == null)
            {
                InititializeCount();
            }
            else  {
                countDown.AddCount();
            }
        }

        public void DecrementCount()
        {
            if (countDown != null)
            {
                countDown.Signal();              
            }
        }

        public void WaitAllToFinish()
        {
            if (countDown!=null)
            {
                countDown.Wait(500);
                if (!countDown.IsSet)
                {
                    throw new TimeoutException("WaitAllToFinish Timeout");
                }
                countDown.Dispose();
                countDown = null;  
            }

        }
    }
}