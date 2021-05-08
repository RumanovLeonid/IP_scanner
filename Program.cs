using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
//Класс для получения своих настроек:
class MySettings
{
    public string myIP, myHost;
    //Конструктор класса с двумя текстовыми аргументами:
    public MySettings(string IP, string Host)
    {
        myIP = IP;
        myHost = Host;
    }
    //Открытый метод для отображения значений поля:
    public void show()
    {
        Console.WriteLine("Мой IP-адресс: {0}, мой Хост: {1}", myIP, myHost);
        Console.WriteLine();
    }
}
//Класс для пинга адреса в сети:
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
        //Поля, содержащие IP адрес и Хост:
        string Host = System.Net.Dns.GetHostName();
        string IP = System.Net.Dns.GetHostByName(Host).AddressList[0].ToString();
        //Создаём объект класса MySettings:
        MySettings settings = new MySettings(IP, Host);
        //Вызываем метод show():
        settings.show();
        Console.WriteLine("Шлюзы:");
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in adapters)
        {
            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
            if (addresses.Count > 0)
            {
                Console.WriteLine(adapter.Description);
                foreach (GatewayIPAddressInformation address in addresses)
                {
                    Console.WriteLine("  Адрес шлюза: {0}",
                        address.Address.ToString());
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine("Активные устройства в подсети:");
        for (int i = 1; i <= 200; i++)
        {
            string ipnum = "192.168.1." + i;
            PingCheck pch = new PingCheck(ipnum);
        }
        Console.WriteLine("Сканирование завершено");
        //Ожидание нажатия какой-нибудь кнопки:
        Console.ReadKey();
    }
}