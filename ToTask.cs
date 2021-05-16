using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IP_scanner
{
    public class ToTask
    {
        private List<string> IP_List = new List<string>();
        private string IPAdress;
        private Task[] tasks = new Task[4];

        public Dictionary<string, string> GetLanHosts()
        {
            Dictionary<string, string> LanHosts = new Dictionary<string, string>();
            IPHostEntry host;

            ScanIP();

            foreach (var item in IP_List)
            {
                try
                {
                    host = Dns.GetHostEntry(item);
                }
                catch (SocketException)
                {
                    LanHosts.Add(item, "UNKNOW HOST");
                    continue;
                }

                LanHosts.Add(item, host.HostName);
            }

            return LanHosts;
        }
        private List<string> ScanIP()
        {
            tasks[0] = Task.Run(ScanIPRange_1);
            tasks[1] = Task.Run(ScanIPRange_2);
            tasks[2] = Task.Run(ScanIPRange_3);
            tasks[3] = Task.Run(ScanIPRange_4);

            Task.WaitAll(tasks);

            IP_List.Sort(CompareIPAdress);

            return IP_List;
        }
        private void ScanIPRange_1()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 1, 63);
        }
        private void ScanIPRange_2()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 64, 127);
        }
        private void ScanIPRange_3()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 128, 191);
        }
        private void ScanIPRange_4()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 191, 254);
        }
        private void ScanRange(ToPing ping, int begin, int end)
        {
            for (int i = begin; i <= end; i++)
            {
                IPAdress = "192.168.1." + i;
                if (ping.Ping(IPAdress) == IPStatus.Success)
                {
                    IP_List.Add(IPAdress);
                }
            }
        }
        private int CompareIPAdress(string x, string y)
        {
            int xHost, yHost, ret;

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
            if (xHost == yHost) {return 0;} else { return 1; };
            
        }
    }
}
