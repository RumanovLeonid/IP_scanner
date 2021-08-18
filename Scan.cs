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
        private readonly List<string> IP_List = new List<string>();
        private string IPAdress;
        private string SubNetMask;
        private readonly string[] arg;
        private readonly Task[] tasks = new Task[4];

        public Scan(string[] args)
        {
            arg = args;
        }

        public Dictionary<string, string> ReturnHostInfo()
        {
            return Run();
        }
        private Dictionary<string, string> Run()
        {
            Dictionary<string, string> IPRangeInformation = new Dictionary<string, string>();
            IPHostEntry host;

            ScanIPRange();

            foreach (var item in IP_List)
            {
                try
                {
                    host = Dns.GetHostEntry(item);
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
        
        private void Range(int range)
        {
            ToPing ping = new ToPing();

            switch(range)
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
                yHost = Convert.ToInt32(y.Substring(10, 2));
            }

            if (xHost < yHost) return -1;
            if (xHost == yHost) { return 0; } else { return 1; };

        }

    }
}
