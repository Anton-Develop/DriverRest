using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DriverRest.Services
{
    public class TCP_Client
    {
        private static bool accept { get; set; }
        public  void  Connect(int port, string server, byte[] data, out byte[] buffer,  out string MessageEX)
        {
            MessageEX = "";
            buffer = new byte[1024];
            try
            {
                TcpClient client = new TcpClient();
               // client.Connect(server, port);
                var result = client.BeginConnect(server, port, null, null);
                accept = true;
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                if (success )
                {
                    while (true)
                    {
                        StringBuilder response = new StringBuilder();
                        NetworkStream stream = client.GetStream();
                        if (data is not null)
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        if (stream.DataAvailable)
                        {
                            do
                            {
                                stream.Read(buffer, 0, buffer.Length);


                            }
                            while (stream.DataAvailable); // пока данные есть в потоке
                        }

                        // Закрываем потоки
                        stream.Close();
                    }
                }
                else
                {
                    Console.WriteLine("Server not found");
                    MessageEX = "Табло не отвечает";
                }
                client.EndConnect(result);
                client.Close();
                
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            
        }
        

    }
    
}

