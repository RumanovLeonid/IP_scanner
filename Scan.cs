using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IP_scanner
{
    public class Scan
    {
        #region Поля класса
        private readonly List<string> IP_List = new List<string>();
        private string IPAdress;
        private string SubNetMask;
        private readonly string[] arg;
        private readonly Task[] tasks = new Task[4];
        #endregion

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        public Scan(string[] args)
        {
            arg = args;
        }

        /// <summary>
        /// Получение информации ключ-значение о хостах в сети
        /// </summary>
        /// <returns>Информацию об адресах и именах хостов</returns>
        public Dictionary<string, string> ReturnHostInfo()
        {
            Dictionary<string, string> IPRangeInformation = new Dictionary<string, string>();
            IPHostEntry host;

            ScanIPRange();

            foreach (var item in ArrangeIP_List())
            {
                try
                {
                    host = Dns.GetHostEntry(item.Trim());
                }
                catch (SocketException)
                {
                    IPRangeInformation.Add(item, "UNKNOW HOST");
                    continue;
                }

                IPRangeInformation.Add(item, host.HostName);
            }

            return IPRangeInformation;
        }

        /// <summary>
        /// Выравнивание списка адресов по левому краю
        /// </summary>
        /// <returns>Выравненный по левому краю список адресов</returns>
        private List<string> ArrangeIP_List()
        {
            List<string> _ = new List<string>();
            
            foreach(var item in IP_List)
            {
                _.Add(item.PadRight(15));
            }

            return _;
        }

        /// <summary>
        /// Сканирование адресов
        /// </summary>
        /// <returns>Отсортированный список адресов</returns>
        private List<string> ScanIPRange()
        {
            if (arg.Length == 0)
            {
                SubNetMask = "192.168.1" + ".";

                tasks[0] = Task.Run(() => { Range(1); });
                tasks[1] = Task.Run(() => { Range(2); });
                tasks[2] = Task.Run(() => { Range(3); });
                tasks[3] = Task.Run(() => { Range(4); });
                Task.WaitAll(tasks);
            }
            else
            {
                Range(0);
            }

            IP_List.Sort(CompareIPAdress);

            return IP_List;
        }

        /// <summary>
        /// Сканирование диапазонов адресов
        /// </summary>
        /// <param name="range">Диапазон адресов</param>
        private void Range(int range)
        {
            ToPing ping = new ToPing();

            switch (range)
            {
                case 0:
                    SubNetMask = arg[0] + ".";
                    int begin = Convert.ToInt16(arg[1]);
                    int end = Convert.ToInt16(arg[2]);

                    ScanRange(ping, begin, end);
                    break;
                case 1:
                    ScanRange(ping, 1, 63);
                    break;
                case 2:
                    ScanRange(ping, 64, 127);
                    break;
                case 3:
                    ScanRange(ping, 128, 191);
                    break;
                case 4:
                    ScanRange(ping, 192, 254);
                    break;
            }
        }

        /// <summary>
        /// Получение адреса хостов из заданного диапазона
        /// </summary>
        /// <param name="ping">Поток</param>
        /// <param name="begin">Начало диапазона</param>
        /// <param name="end">Конец диапазона</param>
        private void ScanRange(ToPing ping, int begin, int end)
        {
            for (int i = begin; i <= end; i++)
            {
                IPAdress = SubNetMask + i;
                if (ping.Ping(IPAdress) == IPStatus.Success)
                {
                    IP_List.Add(IPAdress);
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод для сортировки адресов
        /// </summary>
        /// <param name="x">Первый адрес</param>
        /// <param name="y">Второй адрес</param>
        /// <returns>Результат сравнения</returns>
        private int CompareIPAdress(string x, string y)
        {
            int xHost, yHost;

            if (x.Length == 13)
            {
                xHost = Convert.ToInt32(x.Substring(10, 3));
            }
            else
            {
                xHost = Convert.ToInt32(x.Substring(10, 2));
            }

            if (y.Length == 13)
            {
                yHost = Convert.ToInt32(y.Substring(10, 3));
            }
            else
            {
                if (y.Length == 11)
                {
                    yHost = Convert.ToInt32(y.Substring(10, 1));
                }
                else
                {
                    yHost = Convert.ToInt32(y.Substring(10, 2));
                }
            }

            if (xHost < yHost) return -1;
            if (xHost == yHost) { return 0; } else { return 1; };
        }
    }
}
