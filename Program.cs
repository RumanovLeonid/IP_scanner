using System;

namespace IP_scanner
{
    class IP_Scaner
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Активные устройства в сети:");
            Console.WriteLine("---------------------------");
            
            Scan scan = new Scan(args);

            foreach (var item in scan.ReturnHostInfo())
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Сканирование завершено");
            Console.ReadKey();
        }
    }
}

