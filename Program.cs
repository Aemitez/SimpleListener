using SimpleListener.Properties;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace SimpleListener
{
    internal class Program
    {
        private static void Main()
        {

           
            try
            {
                SocketRecv sc = new SocketRecv();
                Thread tr = new Thread(() => sc.StartListener()) { IsBackground = true };
                tr.Start();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
