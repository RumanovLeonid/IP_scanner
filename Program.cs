using System;
using System.Threading;
using System.Net.NetworkInformation;

class IP_Scaner
{
    public static void Main()
    {
        //Thread tread_1_to_125 = new Thread();
        //Thread tread_126_to_254 = new Thread();

        Console.WriteLine("Активные устройства в подсети:");
        for (int i = 1; i <= 254; i++)
        {
            string ipnum = "192.168.1." + i;
            PingCheck(ipnum);
        }
        Console.WriteLine("Сканирование завершено");

        Console.ReadKey();
    }

    public static void PingCheck(string A)
    {
        Ping ping = new Ping();
        PingReply pingReply = null;
        string server = A;
        pingReply = ping.Send(server);
        if (pingReply.Status == IPStatus.Success)
        {
            Console.WriteLine(server);
            Console.WriteLine(pingReply.Status);
            Console.WriteLine();
        }
    }
}