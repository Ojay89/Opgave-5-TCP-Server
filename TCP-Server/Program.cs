using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Opret server
            IPAddress ip = IPAddress.Parse("192.168.24.241");
            //Opret adresse eller port
            TcpListener serverSocket = new TcpListener(ip, 4646);
            //starter server
            serverSocket.Start();
            Console.WriteLine("Server Started");

            do
            {
                Task.Run(() =>
                {
                    //Venter på connection før den aktiverer
                    TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Server activated & Connected");
                    //kalder metoden DoClient
                    DoClient(connectionSocket);

                });

            } while (true);

        }
        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = sr.ReadLine();
            string answer = "";

            while (message != null && message != "")
            {
                Console.WriteLine("Get all" + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
            }

            ns.Close();
            socket.Close();

        }
    }
}