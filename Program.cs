using System;
using System.Collections.Generic;

namespace IP_scanner
{
    class IP_Scaner
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Активные устройства в подсети:");
            
            Scan scan = new Scan(args);
            Dictionary<string,string> result= scan.Run();
            
            foreach(var item in result)
            {
                Console.WriteLine(item.Key+" "+item.Value);
            }

            Console.WriteLine("Сканирование завершено");
            Console.ReadKey();
        }

    }
}

