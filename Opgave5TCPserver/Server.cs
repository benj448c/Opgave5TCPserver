using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Opgave5TCPserver
{
    public class Server
    {

        private List<Bog> boglist;

        public Server()
        {
            boglist = new List<Bog>();

            boglist.Add(new Bog()
            {
                Titel = "Det Sorte Svaerd", Forfatter = "Benjamin Soerensen", Isbn = "1234567890123", SideTal = 200
            });

            boglist.Add(new Bog()
            {
                Titel = "Det Sorte Svaerd 2", Forfatter = "Benjamin Soerensen", Isbn = "1234567890432", SideTal = 100
            });

            boglist.Add(new Bog()
            {
                Titel = "Det Sorte Svaerd 2.2", Forfatter = "Benjamin Soerensen", Isbn = "4324567890123", SideTal = 150
            });

            boglist.Add(new Bog()
            {
                Titel = "Det Sorte Svaerd Slutningen", Forfatter = "Benjamin Soerensen", Isbn = "1234567812345", SideTal = 999
            });
        }

        public void DoClient(TcpClient socket)
        {
            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                string line;
                while (true)
                {
                    line = sr.ReadLine();

                    Console.WriteLine("Client: " + line);

                    string[] str = line.Split(' ');

                    if (str[0] == "GetAll")
                    {
                        string boger = JsonConvert.SerializeObject(boglist);

                        Console.WriteLine(socket.Client.RemoteEndPoint + ": " + boger);
                        sw.WriteLine(boger);
                    }

                    if (str[0] == "Get")
                    {
                        string boger = JsonConvert.SerializeObject(boglist.FindAll(bog => bog.Isbn == str[1]));
                        Console.WriteLine(socket.Client.RemoteEndPoint + ": " + boger);
                        sw.Write(boger);
                    }

                    if (str[0] == "Save")
                    {
                        boglist.Add(JsonConvert.DeserializeObject<Bog>(str[1]));
                        sw.Write(str[1] + "saved");
                    }
                }
            }
        }

    public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 4646);
            listener.Start();
            Console.WriteLine("Server started listening");
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Accepted client");
                Task.Run((() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                }));
            }
        }
    }
}

