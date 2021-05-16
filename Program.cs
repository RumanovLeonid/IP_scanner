using System;
using System.Collections.Generic;

namespace IP_scanner
{
    class IP_Scaner
    {
        public static void Main()
        {

            Console.WriteLine("Активные устройства в подсети:");
            
            ToTask scan = new ToTask();
            Dictionary<string,string> result= scan.GetLanHosts();
            
            foreach(var item in result)
            {
                Console.WriteLine(item.Key+" "+item.Value);
            }

            Console.WriteLine("Сканирование завершено");
            Console.ReadKey();
        }

    }
}

