using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DriverRest.Services
{
    
        public class TcpHelper
        {
            private static TcpListener listener { get; set; }
            private static bool accept { get; set; }
            
        public  void StartServer(string IP_Address,int port)
            {
                try
                {
                    IPAddress address = IPAddress.Parse(IP_Address);
                    listener = new TcpListener(address, port);
                    listener.Start();
                    accept = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fault Start server TCP_IP:__"+ex);
                }
                finally
                {
                    Listen();
                    listener.Stop();
                }

            
        }
            public static void Listen()
            {
                try
                {
                    if (listener != null && accept)
                    {
                        while (true)
                        {
                            Console.WriteLine("Waiting connection...");
                            var clientTask = listener.AcceptTcpClientAsync();
                            if (clientTask.Result != null)
                            {
                                var client = clientTask.Result;
                                string message = " ";

                                while (message != null && !message.StartsWith("quit"))
                                {
                                    try
                                    {
                                        byte[] data = Encoding.ASCII.GetBytes("Send next data:[enter 'quit' to terminate]");
                                        client.GetStream().Write(data, 0, data.Length);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine("Failed Write byte data:____" + ex);
                                    }
                                    try
                                    {
                                        byte[] buffer = new byte[1024];
                                        client.GetStream().Read(buffer, 0, buffer.Length);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine("Failed Read byte data:____" + ex);
                                    }
                                }
                                client.GetStream().Dispose();

                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Send or Read Error failed:__" + ex);
                }
            }
        }
       

    }



