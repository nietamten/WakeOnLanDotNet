using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3 || args[0].Length != 12)
            {
                System.Console.Write("uage: app.exe mac_without_separators host_name port\n");
                System.Console.Write("example: wol.exe 001B781D802A biuro.pl 7000\n");
                return;
            }

            Byte[] mac = new Byte[6];
            for (int i = 0; i < 6; i++)
                mac[i] = Convert.ToByte(args[0].Substring(i*2,2),16);
    
            int port = Convert.ToInt32(args[2], 10);
            
            Byte[] sendBytes = new Byte[17 * 6];
            for (int i = 0; i < 6; i++)
                sendBytes[i] = 0xff;
            for (int i = 6; i < 17*6; i++)
                sendBytes[i] = mac[i%6];

            System.Net.IPAddress []ips = System.Net.Dns.GetHostAddresses(args[1]);

            if ( ips.Length > 0)
            {
                System.Net.IPAddress ip = ips[0];
                System.Net.IPEndPoint ipep = new System.Net.IPEndPoint(ip, port);
                System.Net.Sockets.UdpClient udpc = new System.Net.Sockets.UdpClient();

                udpc.Send(sendBytes, sendBytes.Length, ipep);
                System.Console.Write("ok\n");
            }
            else
            {
                System.Console.Write("error host not found\n");
            }
        }
    }
}