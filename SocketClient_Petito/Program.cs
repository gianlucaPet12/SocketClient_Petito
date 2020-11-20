using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient_Petito
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string strIPAddress = "";
            string strPort = "";
            IPAddress ipAddress = null;
            int nPort;

            try
            {
                Console.WriteLine("IP del server: ");
                strIPAddress = Console.ReadLine();
                Console.WriteLine("Porta del server");
                strPort = Console.ReadLine();

                if (!IPAddress.TryParse(strIPAddress.Trim(), out ipAddress))
                {
                    Console.WriteLine("IP non valido.");
                    return;
                }

                if (!int.TryParse(strPort, out nPort))
                {
                    Console.WriteLine("La porta non è valida");
                    return;
                }
                  
                if (nPort <= 0 || nPort >= 65535)
                {
                    Console.WriteLine("Porta non valida");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("EndPoint del server" + ipAddress.ToString() + " " + nPort);

                client.Connect(ipAddress, nPort);

                byte[] buff = new byte[128];
                string SendString = "";
                string receiveString = "";
                int receviedBytes = 0;

                while (true)
                {
                    Console.WriteLine("Manda un messaggio");
                    SendString = Console.ReadLine();
                    buff = Encoding.ASCII.GetBytes(SendString);
                    client.Send(buff);
                    if(SendString.ToUpper().Trim () ==  "QUIT")
                    {
                        break;
                    }
                    Array.Clear(buff, 0, buff.Length);
                    receviedBytes = client.Receive(buff);
                     
                    Console.WriteLine("SI: " + receiveString);
                     

                 }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

            Console.ReadLine();
        }
    }
}
