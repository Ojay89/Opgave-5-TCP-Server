using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCP_Server.SimpleRestService;

namespace TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Opret server
            IPAddress ip = IPAddress.Parse("10.135.32.41");
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

            Book returnBook = JsonConvert.DeserializeObject<Book>(sr.ReadLine());
            
            string message = sr.ReadLine();
            string answer = "";

            while (message != null && message != "")
            {
                Console.WriteLine("Book info" + "" + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
                returnBook= JsonConvert.DeserializeObject<Book>(sr.ReadLine());
                message = returnBook.ToString();
            }

            ns.Close();
            socket.Close();

        }
    }
}