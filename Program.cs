using System;
using System.Threading;
using System.Net.NetworkInformation;

class IP_Scaner
{
    static Ping ping = new Ping();
    static PingReply pingReply = null;

    public static void Main()
    {
        Console.WriteLine("Активные устройства в подсети:");

        for (int i = 1; i <= 254; i++)
        {
            string ipnum = "192.168.1." + i;
            PingCheck(ipnum);
        }

        Console.WriteLine("Сканирование завершено");

        Console.ReadKey();
    }

    public static void ScanIPRange(int BeginRange, int EndRange)
    {
        for (int i = BeginRange; i <= EndRange; i++)
        {
            string ipnum = "192.168.1." + i;
            PingCheck(ipnum);
        }
    }

    public static void PingCheck(string A)
    {
        pingReply = ping.Send(A, 5);

        if (pingReply.Status == IPStatus.Success)
        {
            Console.Write(A);
            Console.WriteLine(" " + pingReply.Status);
        }
        else
        {
            Console.Write(A);
            Console.WriteLine(" "+ IPStatus.NoResources);
        }
    }
}