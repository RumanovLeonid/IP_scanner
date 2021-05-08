using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

class PingCheck
{
    //Конструктор класса с одним аргументом:
    public PingCheck(string A)
    {
        Ping ping = new Ping();
        PingReply pingReply = null;
        string server = A;
        pingReply = ping.Send(server);
        if (pingReply.Status == IPStatus.Success)
        {
            Console.WriteLine(server); //server          
            Console.WriteLine(pingReply.Status); //Статус           
            Console.WriteLine();
        }
    }
}
//Класс с главным методом программы:
class NetworkSettingsDemo
{
    //Главный метод программы:
    public static void Main()
    {
        Console.WriteLine("Активные устройства в подсети:");
        for (int i = 1; i <= 254; i++)
        {
            string ipnum = "192.168.1." + i;
            PingCheck pch = new PingCheck(ipnum);
        }
        Console.WriteLine("Сканирование завершено");
        //Ожидание нажатия какой-нибудь кнопки:
        Console.ReadKey();
    }
}