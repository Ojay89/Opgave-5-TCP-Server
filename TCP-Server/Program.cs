using System;
using System.Collections.Generic;
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
        private static readonly List<Book> books = new List<Book>()
        {
            new Book("Omars Bog", "Omar", 124, "1294837261729"),
            new Book("Olivers Bog", "Oliver", 581, "2189271629471"),
            new Book("Talias Bog", "Talia", 183, "4910481634910"),
            new Book("Konrads Bog", "Konrad", 193, "0494820184613"),
            new Book("Jamshids bog", "Jamshid", 981, "1239481638184")
        };

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
            
            //string answer = "";

            while (message != null && message != "")
            {
              
                string[] messageArray = message.Split(' ');
                string param = message.Substring(message.IndexOf(' ') + 1);
                string command = messageArray[0];

                switch (command)
                {
                    case "GetAll":
                        sw.WriteLine("Get all received");   
                        sw.WriteLine(JsonConvert.SerializeObject(books));
                        break;
                    case "Get":
                        sw.WriteLine("Get request received -- isbn" + messageArray[1] + "requested");
                        sw.WriteLine(JsonConvert.SerializeObject(books.Find(id => id.Isbn13 == param)));
                        break;
                    case "Save":
                        sw.WriteLine("Save received");
                        Book saveBook = JsonConvert.DeserializeObject<Book>(param);
                        books.Add(saveBook);
                        break;
                    default: 
                        sw.WriteLine("Fejl i søgning!");
                        break;
                }
                     message = sr.ReadLine();
            }


            ns.Close();
            socket.Close();

        }
    }
}