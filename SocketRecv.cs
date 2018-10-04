using SimpleListener.Properties;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleListener
{
    internal class SocketRecv
    {
        public void StartListener()
        {
            try
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Parse(Settings.Default.Listen_Server), Settings.Default.Listen_Port);
                serverSocket.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, false);
                TcpClient clientSocket = default(TcpClient);
                serverSocket.Start();
                WriteLogReceive(string.Format(@"--->> Listener service started on port : {0} <<---", Settings.Default.Listen_Port));

                while (true)
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    clientSocket.ReceiveTimeout = Settings.Default.Socket_Timeout;

                    Thread trd = new Thread(() => StartClient(clientSocket)) { IsBackground = true };
                    trd.Start();
                }
            }
            catch (Exception exListener)
            {
                WriteLogReceive(@"Failed to start Listener, " + exListener.Message);
            }
        }

        private void StartClient(TcpClient incomingClient)
        {
            var inClient = incomingClient;
            var ipClient = inClient.Client.RemoteEndPoint.ToString();
            var maxBuff = Settings.Default.Listen_Length;
            var bytesFrom = new byte[maxBuff];
            WriteLogReceive(@"Incoming client : " + ipClient);

            while (true)
            {
                try
                {
                    if (inClient.Client.Connected == false)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    var stremEncoding = Settings.Default.Socket_Encode;

                    NetworkStream networkStream = inClient.GetStream();
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    var dataFromClient = Encoding.GetEncoding(stremEncoding).GetString(bytesFrom).TrimEnd('\0');

                    dataFromClient = dataFromClient.Trim();

                    var strReply = "OK";
                    var serverResponse = string.Format(@"Client : [{0}], Ack : [{1}], DataResult : [{2}]", ipClient, strReply, dataFromClient);

                    WriteLogReceive(serverResponse);

                    var sendBytes = Encoding.GetEncoding(stremEncoding).GetBytes(strReply);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();

                    Thread.Sleep(10);
                    inClient.Client.Close();
                    inClient = null;

                    WriteLogReceive(string.Format(@"Close connection for client : [{0}]", ipClient));
                    return;
                }
                catch (Exception e2)
                {
                    Thread.Sleep(10);
                    if (inClient != null)
                    {
                        inClient.Client.Close();
                        inClient = null;
                    }
                    WriteLogReceive(string.Format(@"Error StartClient : {0}", e2.Message));
                    return;
                }
            }
        }

        private void WriteLogReceive(string logtext)
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine(logtext);
            }
        }
    }
}