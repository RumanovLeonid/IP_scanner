using System;
using System.Threading;
using System.Net.NetworkInformation;

class IP_Scaner
{
    public class Pinger
    {
        private Ping ping = new Ping();
        private PingReply pingReply;
        public IPStatus Ping(string IPAdress)
        {
            pingReply = ping.Send(IPAdress, 5);
            return pingReply.Status;
        }
    }
    public static void Main()
    {
        Console.WriteLine("Активные устройства в подсети:");

        Console.WriteLine("Сканирование завершено");
        Console.ReadKey();
    }
    public static void ScanTask_1()
    {
        Pinger ping = new Pinger();
        string IPAdress;

        for (int i = 1; i <= 100; i++)
        {
            IPAdress = "192.168.1." + i;
            if (ping.Ping(IPAdress) == IPStatus.Success)
            {
                Console.Write(IPAdress);
                Console.WriteLine(" " + IPStatus.Success);
            }
            else
            {
                Console.Write(IPAdress);
                Console.WriteLine(" " + IPStatus.NoResources);
            }
        }
    }
}
